using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;
using Microsoft.ConfigurationManagement.ManagementProvider;
using System.Windows.Forms.VisualStyles;
using System.IO;
using System.Net;
using Windows7.DesktopIntegration.WindowsForms;

namespace RolloutLister
{
    public partial class Main : Form
    {

        public class ACF2
        {
            public String ID { get; set; }
            public List<NodeItem> Nodes { get; set; }
            public List<NodeItem> SelectedNodes { get; set; }
            public String log { get; set; }
        }

        public class Rejections
        {
            public String ID { get; set; }
        }

        public class NodeItem
        {
            public string Node { get; set; }
            public string Active { get; set; }
            public string Last_Login { get; set; }
            public String[] Agents { get; set; }
            public String[] dateString { get; set; }
            public String HeartBeat { get; set; }
            public String DateCreated { get; set; }
            public String Site { get; set; }
            public String MachineUse { get; set; }
            public String Owner { get; set; }
            public String OS { get; set; }   
        }

        WqlConnectionManager scope;
        String[] ACF2s1; //Used for background thread
        String[] ACF2s2; // Used for second background thread
        List<ACF2> active_ACF2;
        List<Rejections> rejections;
        String current_User;
        private BackgroundWorker _backgroundWorker;
        private BackgroundWorker _backgroundWorker2;
        DateTime timer;
        int totalACF2s;
        int completed = 0;
        object sem = new object();
        long totalTimeTaken;
        int safety = 1;
        String server = "sv72902";
        //Settings when exporting information
        bool exportRejection_List = false;
        bool exportNode_List = false;
        bool exportSelection_Log = false;
        

        public Main()
        {
            
            InitializeComponent();
            scope = Connect("\\"+server);
           active_ACF2 = new List<ACF2>();
            rejections = new List<Rejections>();
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            _backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);

            _backgroundWorker2 = new BackgroundWorker();
            _backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            _backgroundWorker2.WorkerReportsProgress = true;
            _backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            _backgroundWorker2.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            
        }

        public WqlConnectionManager Connect(string serverName)
        {
            try
            {
                SmsNamedValuesDictionary namedValues = new SmsNamedValuesDictionary();
                WqlConnectionManager connection = new WqlConnectionManager(namedValues);

                if (System.Net.Dns.GetHostName().ToUpper() == serverName.ToUpper())
                {
                    // Connect to local computer.
                    connection.Connect(serverName);
                }
                else
                {
                    // Connect to remote computer.
                    connection.Connect(serverName);
                }

                return connection;
            }
            catch (SmsException e)
            {
                MessageBox.Show("Failed to Connect.\nPlease check the server address you are attempting.\n\n Error: " + e.Message,
                    "Error Occured",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    0);
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show("Failed to authenticate.\nPlease ensure you have access.\n\n Error:" + e.Message,
                    "Error Occured",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    0);
                return null;
            }
        }

        public NodeItem populateItemWin7(Dictionary<String, String> ID)
        {
            String datesString = ID["AgentTime"];
            String[] dates = datesString.Split(',');
            String[] agents = ID["AgentName"].Split(',');
            int heartbeat_Index = -1;
            DateTime latestDate = new DateTime(0);

            if (ID["TotalConsoleTime"] == "" || ID["TotalConsoleTime"] == "0")
            {
                return populateItemWinXP(ID);
            }
            //Console.WriteLine("Console Minutes = " + ID["TotalUserConsoleMinutes"] + " Total Time = " + ID["TotalConsoleTime"]);
            int consolePercent = Convert.ToInt32(double.Parse(ID["TotalUserConsoleMinutes"]) / double.Parse(ID["TotalConsoleTime"])*100);
            
            if (consolePercent <= 66)
                return null;

            //Console.WriteLine("Console time is " + consolePercent + " " + double.Parse(ID["TotalUserConsoleMinutes"]) + " / " + double.Parse(ID["TotalConsoleTime"]));
            //Search for the most recent heartbeat... Sometimes there are two, ignore the second
            //Console.WriteLine(ID["Name"]);
            //Console.WriteLine("Checking agents");

            for (int i = 0; i < agents.Length; i++ )
            {
                if (agents[i] == "{Heartbeat Discovery}")
                {
                    if (latestDate.CompareTo(Convert.ToDateTime(dates[i])) < 0)
                    {
                        latestDate = Convert.ToDateTime(dates[i]);
                        heartbeat_Index = i;
                    }
                    
                }
                
            }
           // Console.WriteLine("Finished checking Agents");

            //Heartbeat was not found. Replace the heartbeat value with a replacement
            //Console.WriteLine("Checking HeartBeats");
            if (heartbeat_Index == -1)
            {
                DateTime replacement = new DateTime();
                heartbeat_Index = 0;
                dates[heartbeat_Index] = replacement.ToString();
            }
            //Console.WriteLine("Done Checking HeartBeats = " + dates[heartbeat_Index]);
            NodeItem item = new NodeItem { Node = ID["NetbiosName"],
                Active = ID["Active"],
                Last_Login = ID["LastLogonUserName"],
                Owner = ID["employeeID"],
                Site = ID["SiteName"],
                HeartBeat = dates[heartbeat_Index],
                dateString = dates,
                DateCreated = ID["CreationDate"],
                Agents = agents,
                OS = ID["OperatingSystemNameandVersion"],
                MachineUse = consolePercent.ToString()};

            //Console.WriteLine("Finished populating item");
            return item;
        }

        public NodeItem populateItemWinXP(Dictionary<String, String> ID)
        {
            String datesString = ID["AgentTime"];
            String[] dates = datesString.Split(',');
            String[] agents = ID["AgentName"].Split(',');
            int heartbeat_Index = -1;
            DateTime latestDate = new DateTime(0);

            //Search for the most recent heartbeat... Sometimes there are two, ignore the second
            //Console.WriteLine(ID["Name"]);
            //Console.WriteLine("Checking agents");

            for (int i = 0; i < agents.Length; i++)
            {
                if (agents[i] == "{Heartbeat Discovery}")
                {
                    if (latestDate.CompareTo(Convert.ToDateTime(dates[i])) < 0)
                    {
                        latestDate = Convert.ToDateTime(dates[i]);
                        heartbeat_Index = i;
                    }

                }

            }
            // Console.WriteLine("Finished checking Agents");

            //Heartbeat was not found. Replace the heartbeat value with a replacement
            //Console.WriteLine("Checking HeartBeats");
            if (heartbeat_Index == -1)
            {
                DateTime replacement = new DateTime();
                heartbeat_Index = 0;
                dates[heartbeat_Index] = replacement.ToString();
            }
            //Console.WriteLine("Done Checking HeartBeats = " + dates[heartbeat_Index]);
            NodeItem item = new NodeItem
            {
                Node = ID["NetbiosName"],
                Active = ID["Active"],
                Last_Login = ID["LastLogonUserName"],
                Site = ID["SiteName"],
                Owner = ID["employeeID"],
                HeartBeat = dates[heartbeat_Index],
                dateString = dates,
                DateCreated = ID["CreationDate"],
                Agents = agents,
                OS = ID["OperatingSystemNameandVersion"],
                MachineUse = "Unknown"
            };

            //Console.WriteLine("Finished populating item");
            return item;
        }

        //Display the ACF2 ID's information
        public void displayACF2(String id)
        {
            try
            {
                acf2_Box.Text = "ACF2: " + id;
                String[] splitter = id.Split(' ');
                var dataSource = new List<NodeItem>(active_ACF2.Find(
                delegate (ACF2 user)
                {
                    return user.ID == splitter[0];   
                }).Nodes);
                nodeList.DataSource = dataSource;
                set_Selected(splitter[0]);
                current_User = splitter[0];
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to display Table.\n\nError: " + ex.Message,
                    "Error Occured",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    0);
                //throw;
            }
        }

        public void QueryACF2s(WqlConnectionManager connection,String[] ACF2s,int subThread)
        {
            try
            {
                int i = 1;
                foreach (String id in ACF2s)
                {

                    int counter = 0;
                    List<NodeItem> nodes = new List<NodeItem>();
                    IResultObject query = scope.QueryProcessor.ExecuteQuery                    
                  (@"Select distinct 
                    SMS_R_SYSTEM.NetbiosName,
                    SMS_R_SYSTEM.OperatingSystemNameandVersion,
                    SMS_R_SYSTEM.CreationDate,
                    SMS_SITE.SiteName,
                    SMS_R_SYSTEM.Active,
                    SMS_R_SYSTEM.LastLogonUserName,
                    SMS_R_SYSTEM.employeeId,
                    SMS_R_SYSTEM.AgentTime,
                    SMS_R_SYSTEM.AgentName, 
                    SMS_G_SYSTEM_COMPUTER_SYSTEM.Model, 
                    SMS_G_SYSTEM_SYSTEM_CONSOLE_USER.NumberOfConsoleLogons, 
                    SMS_G_SYSTEM_SYSTEM_CONSOLE_USER.TotalUserConsoleMinutes,
                    SMS_G_SYSTEM_SYSTEM_CONSOLE_USAGE.TotalConsoleTime,
                    SMS_G_SYSTEM_SYSTEM_CONSOLE_USER.LastConsoleUse 
                    from SMS_G_SYSTEM_SYSTEM_CONSOLE_USER 
                    INNER JOIN SMS_R_SYSTEM on SMS_R_SYSTEM.ResourceID = SMS_G_SYSTEM_SYSTEM_CONSOLE_USER.ResourceID 
                    LEFT JOIN SMS_G_SYSTEM_SYSTEM_CONSOLE_USAGE on SMS_G_SYSTEM_SYSTEM_CONSOLE_USAGE.ResourceID = SMS_G_SYSTEM_SYSTEM_CONSOLE_USER.ResourceID 
                    LEFT JOIN SMS_G_SYSTEM_SYSTEM_ENCLOSURE on SMS_G_SYSTEM_SYSTEM_ENCLOSURE.ResourceID = SMS_R_SYSTEM.ResourceID 
                    INNER JOIN SMS_G_SYSTEM_COMPUTER_SYSTEM on SMS_G_SYSTEM_COMPUTER_SYSTEM.ResourceID = SMS_R_SYSTEM.ResourceID
                    left  join SMS_Site on SMS_Site.SiteCode=SMS_R_SYSTEM.SMSAssignedSites 
                    Where SMS_R_SYSTEM.employeeId = '" + id + @"'
                    Or SMS_R_SYSTEM.LastLogonUserName = '" + id + @"' 
                    AND SMS_G_SYSTEM_SYSTEM_CONSOLE_USAGE.TotalConsoleTime != 0");


                    foreach (IResultObject o in query)
                    {
                        foreach (IResultObject thing in o)
                        {
                            Dictionary<String, String> results = new Dictionary<string, string>();
                            List<IResultObject> obj = thing.GenericsArray;
                            foreach (IResultObject obj2 in obj)
                            {


                                foreach (KeyValuePair<string, string> kvpPair in obj2.PropertyList)
                                {
                                    //Console.WriteLine(kvpPair.Value + " " + kvpPair.Key);
                                    results.Add(kvpPair.Key, kvpPair.Value);
                                }

                            }
                            NodeItem item = populateItemWin7(results);
                            if (item != null)
                            {
                                nodes.Add(item);
                                counter++;
                            }
                            //Console.WriteLine();
                        }
                    }

                    /*The above query will not produce any US results
                     * Try and find all results for that user
                     */
                    if (counter == 0)
                    {
                        query = scope.QueryProcessor.ExecuteQuery("Select * From SMS_R_SYSTEM left  join SMS_Site on SMS_Site.SiteCode=SMS_R_SYSTEM.SMSAssignedSites  where NetbiosName = '" + id + "' OR LastLogonUserName = '" + id + "'");
                        foreach (IResultObject o in query)
                        {
                            foreach (IResultObject thing in o)
                            {
                                Dictionary<String, String> results = new Dictionary<string, string>();
                                List<IResultObject> obj = thing.GenericsArray;
                                foreach (IResultObject obj2 in obj)
                                {


                                    foreach (KeyValuePair<string, string> kvpPair in obj2.PropertyList)
                                    {
                                        results.Add(kvpPair.Key, kvpPair.Value);
                                    }

                                }
                                nodes.Add(populateItemWinXP(results));
                                counter++;
                                //Console.WriteLine();
                            }
                        }
                    }
                    Sorter sorter = new Sorter();
                    nodes = sorter.removeDuplicateNodes(nodes);

                    //PERFORM SELECTION LOGIC AND INSERT INTO active ACF2
                    //Console.WriteLine("Making Selections");
                    if (counter > 0)
                    {
                        String log = "";

                        //Way to return two variables to keep track of selection logging
                        //Key will contain the list of Nodes
                        //Value will contain the log
                        KeyValuePair<List<NodeItem>,String> selections = sorter.selectBest(nodes,id,safety, log);
                        
                        nodes = sorter.sortByDescendingDate(nodes);
                        lock (active_ACF2)
                        {
                            active_ACF2.Add(new ACF2 { ID = id, Nodes = nodes, SelectedNodes = selections.Key, log = selections.Value });
                        }
                    }
                    else
                    {
                        //Add node to rejection list
                        lock (rejections)
                        {
                            rejections.Add(new Rejections { ID = id });
                        }
                        
                    }
                    //Console.WriteLine("Selections have been made");


                    counter++;

                    lock (sem)
                    {
                        completed++;
                        String[] objects = new String[3];
                        objects[0] = completed.ToString();
                        objects[1] = id;
                        objects[2] = totalACF2s.ToString();
                        if (subThread == 1)
                            _backgroundWorker.ReportProgress((100 * completed) / totalACF2s, objects);
                        else
                            _backgroundWorker2.ReportProgress((100 * completed) / totalACF2s, objects);
                    }
                    i++;
                }

            }
            catch (SmsException ex)
            {
                Console.WriteLine("Failed to query List: " + ex.Message);
                //throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured: " + ex.Message);
                //throw;
            }
        }

        public void populate_Tree(List<ACF2> users)
        {
            ACF2_Tree.Nodes.Clear();
            foreach(ACF2 user in users){
                List<TreeNode> leaves = new List<TreeNode>();
                bool inactive = false;
                foreach (NodeItem selected in user.SelectedNodes)
                {
                    if (selected.Active == "0" || selected.Active == "")
                        inactive = true;
                    TreeNode leaf = new TreeNode(selected.Node);
                    leaves.Add(new TreeNode(selected.Node));
                }
                TreeNode node = new TreeNode(user.ID + " ("+user.Nodes.Count+")",(TreeNode[])leaves.ToArray());
                if (inactive == true)
                    node.BackColor = Color.Red;
                ACF2_Tree.Nodes.Add(node);
            }
        }

        //Update the tree after a selection change has been made for visual recognition
        public void update_Tree(ACF2 user)
        {
            bool inactive = false;
            List<TreeNode> leaves = new List<TreeNode>();
            foreach (NodeItem selected in user.SelectedNodes)
            {
                if (selected.Active == "0" || selected.Active == "")
                    inactive = true;
                leaves.Add(new TreeNode(selected.Node));
            }

            TreeNodeCollection collection = ACF2_Tree.Nodes;
            int index = 0;
            foreach (TreeNode node in collection)
            {
                String[] splits = node.Text.Split(' ');
                if (splits[0] == current_User)
                    index = node.Index;
            }
            
            ACF2_Tree.Nodes.RemoveAt(index);
            TreeNode node2 = new TreeNode(user.ID +" ("+user.Nodes.Count+")", (TreeNode[])leaves.ToArray());

            //Alert the user that there could be a issue with the selections made on a specific ACF2
            if (inactive == true)
                node2.BackColor = Color.Red;

            ACF2_Tree.Nodes.Insert(index,node2);
        }

        private void addACF2ListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manual_Entry form = new Manual_Entry();
            form.ShowDialog();
            String list = form.get();
            //Console.WriteLine(list);
            
            if (list != null)
            {
                ACF2s1 = list.Split(" .,/\\;:!\n\t\r".ToCharArray());
                int index = 0;
                List<String> acf2s = ACF2s1.ToList<String>();
                while (index < (acf2s.Count() - 1))
                {
                    if (acf2s[index] == "")
                        acf2s.RemoveAt(index);
                    else
                        index++;
                }
                totalACF2s = acf2s.Count();

                if (acf2s.Count() > 1)
                {
                    divideACF2_Work(acf2s);
                }
                else
                {
                    ACF2s1 = acf2s.ToArray();
                    ACF2s2 = null;
                }
                LoadList();
            }
            form.Dispose();
        }

        private void LoadList()
        {
            completed = 0;
            save_Selected.Enabled = false;
            ACF2_Tree.Enabled = false;
            menuStrip1.Enabled = false;
            _backgroundWorker.RunWorkerAsync(ACF2s1);
            _backgroundWorker2.RunWorkerAsync(ACF2s2);
            
        }

        //Display the selected ACF2 when selected
        private void ACF2_Tree_AfterSelect(object sender, EventArgs e)
        {
            if (ACF2_Tree.SelectedNode.Parent == null)
            {
                String[] user_Info = ACF2_Tree.SelectedNode.Text.Split(' ');
                displayACF2(user_Info[0]);
                current_User = user_Info[0];
            }
        }


        /*
         * Basic Sort Features
         * Only for specific tabs are currently available
         */
        private void sortButton_Click(object sender, DataGridViewCellMouseEventArgs e)
        {

            List<NodeItem> list = active_ACF2.Find(
                delegate(ACF2 user)
                {
                    return user.ID == current_User;
                }).Nodes;

            Sorter sort = new Sorter();
            if (e.ColumnIndex >= 0 && e.RowIndex == -1)
            {
                if (nodeList.Columns[e.ColumnIndex].Name == "Node")
                {
                    active_ACF2.Find(
                    delegate(ACF2 user)
                    {
                        return user.ID == current_User;
                    }).Nodes = sort.sortByAscendingNode(list);
                    displayACF2(current_User);
                }
                else if (nodeList.Columns[e.ColumnIndex].Name == "HeartBeat")
                {
                    active_ACF2.Find(
                    delegate(ACF2 user)
                    {
                        return user.ID == current_User;
                    }).Nodes = sort.sortByDescendingDate(list);
                    displayACF2(current_User);
                }
                else if (nodeList.Columns[e.ColumnIndex].Name == "DateCreated")
                {
                    active_ACF2.Find(
                    delegate(ACF2 user)
                    {
                        return user.ID == current_User;
                    }).Nodes = sort.sortByDescendingCreation(list);
                    displayACF2(current_User);
                }
            }
        }

        //Get selected items in DataGridView to store in the list
        private void get_Selected()
        {
            //Get the current ACF2 Object
            ACF2 user = active_ACF2.Find(
                delegate(ACF2 acf2)
                {
                    return acf2.ID == current_User;
                }
                );

            List<NodeItem> new_Selections = new List<NodeItem>();
            //Store the newly selected NodeItems into the selected ACF2 Object
            for (int i = 0; i < nodeList.RowCount; i++)
            {
                if (nodeList.Rows[i].Selected == true)
                {
                    new_Selections.Add(user.Nodes.Find(
                        delegate(NodeItem node)
                        {
                            return node.Node == (string) nodeList[0,i].Value;
                        }));  
                }    
            }

            active_ACF2.Find(
                delegate(ACF2 acf2)
                {
                    return acf2.ID == current_User;
                }).SelectedNodes = new List<NodeItem>(new_Selections);

        }

        //Display selected values in DataGridView
        private void set_Selected(String id)
        {
            ACF2 user = active_ACF2.Find(
                delegate(ACF2 acf2)
                {
                    return acf2.ID == id;
                });

            //This is done because when a form is generated, the first row is always selected. This will prevent that selection
            if (user.SelectedNodes.Count > 0)
                nodeList.Rows[0].Selected = false;

            foreach (NodeItem selection in user.SelectedNodes)
            {
                for (int i = 0; i < nodeList.RowCount; i++)
                {
                    if (selection.Node == (string) nodeList[0, i].Value)
                        nodeList.Rows[i].Selected = true;
                }
            }
        }

        //Saves the selection changes
        private void save_Selected_Click(object sender, EventArgs e)
        {
            get_Selected();
            ACF2 user = active_ACF2.Find(
                delegate(ACF2 acf2)
                {
                    return acf2.ID == current_User;
                });
            update_Tree(user);
        }

        /*
         * Dispenses the worker threads to perform the search
         * Each thread will take half of the list
         * Selection logic is also performed on each thread
         */
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            timer = DateTime.Now;
            if(sender == _backgroundWorker)
                QueryACF2s(scope,(String[])e.Argument,1);
            else
                QueryACF2s(scope, (String[])e.Argument, 0);
        }
        
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update the UI here
            //        _userController.UpdateUsersOnMap();
            //Console.WriteLine("Progress = "+e.ProgressPercentage);
            if (e != null)
            {

                int percent = e.ProgressPercentage;

                progressBar.Value = percent;

                //Dont try and update the taskbar on winXP
                if(!Environment.OSVersion.Version.ToString().Contains("5.1"))
                    progressBar.SetTaskbarProgress();

                String[] info = (String[])e.UserState;
                long time_Taken = DateTime.Now.Ticks - timer.Ticks;
                totalTimeTaken += time_Taken;

                long average = totalTimeTaken / completed;
                double seconds = (double)average / 10000 / 1000;
                seconds = seconds * (double.Parse(info[2]) - double.Parse(info[0]));
                int sec = Convert.ToInt32(seconds) % 60;
                int minutes = Convert.ToInt32(seconds) / 60;
                // TimeSpan elapsedSpan = new TimeSpan(time_Taken * int.Parse(info[2]));
                ETAtoolStripStatus.Text = "ETA: " + minutes + ":" + sec;
                //toolStripStatusUser.Text = info[1];
                toolStripStatusCount.Text = info[0] + "/" + info[2];
                timer = DateTime.Now;
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Ensure both have completed before populating the tree
            if (!_backgroundWorker.IsBusy && !_backgroundWorker2.IsBusy)
            {
                ACF2_Tree.Enabled = true;
                active_ACF2 = new Sorter().removeDuplicateACF2(active_ACF2);

                populate_Tree(active_ACF2);

                foreach (Rejections reject in rejections)
                    rejectionBox.Items.Add(reject.ID);

                progressBar.Value = 100;
                save_Selected.Enabled = true;
                menuStrip1.Enabled = true;
                ETAtoolStripStatus.Text = "ETA: 0:00 Minutes";
                foundPage.Text = "Found ACF2's (" + active_ACF2.Count + ")";
                rejectionPage.Text = "Rejections (" + rejections.Count + ")";
                completed = 0;
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = exporter.ShowDialog();

            String today = DateTime.Now.ToShortDateString();
            today = today.Replace('/', '-');

            if (exporter.SelectedPath != "" && result == DialogResult.OK)
            {

                if (exportRejection_List == true)
                {
                    StreamWriter swr = File.AppendText(exporter.SelectedPath.ToString() + @"\rejectionList-" + today + ".csv");
                    foreach (Rejections reject in rejections)
                    {
                        swr.WriteLine(reject.ID);
                    }
                    swr.Close();
                }

                if (exportNode_List == true)
                {
                    StreamWriter swn = File.AppendText(exporter.SelectedPath.ToString() + @"\nodeList-" + today + ".csv");

                    foreach (ACF2 user in active_ACF2)
                    {
                        swn.Write(user.ID.ToLower());
                        foreach (NodeItem selected in user.Nodes)
                        {
                            swn.Write("," + selected.Node + "," + selected.Site + "," + selected.Active);
                            swn.WriteLine();
                        }
                    }
                    swn.Close();
                    
                }

                if (exportSelection_Log == true)
                {
                    StreamWriter swl = File.AppendText(exporter.SelectedPath.ToString() + @"\selectionLog-" + today + ".txt");

                    foreach (ACF2 user in active_ACF2)
                    {
                        swl.WriteLine(user.log);
                    }
                    swl.Close();

                }

                StreamWriter sw = File.AppendText(exporter.SelectedPath.ToString() + @"\rollout-"+today+".csv");

                foreach (ACF2 user in active_ACF2)
                {
                    
                    foreach (NodeItem selected in user.SelectedNodes)
                    {
                        sw.Write(user.ID.ToLower());
                        sw.Write(",");
                        sw.WriteLine(selected.Node);
                    }
                }
                sw.Close();

                MessageBox.Show("Export Finished: rollout-" + today + ".csv",
                    "Export Rollout List",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    0);
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = importer.ShowDialog();

            if (importer.FileName != "" && result == DialogResult.OK)
            {
                String list = null;
                try
                {
                    StreamReader sr = File.OpenText(importer.FileName);

                    list = sr.ReadToEnd();
                    sr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to open file.\n\nError: " + ex.Message,
                    "Error Occured",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    0);
                }
                if (list != null)
                {
                    ACF2s1 = list.Split(" .,/\\;:!\n\r".ToCharArray());
                    int index = 0;
                    List<String> acf2s = ACF2s1.ToList<String>();

                    while (index < (acf2s.Count() - 1))
                    {
                        if (acf2s[index] == "")
                            acf2s.RemoveAt(index);
                        else
                            index++;
                    }

                    totalACF2s = acf2s.Count();

                    if (acf2s.Count() > 1)
                    {
                        divideACF2_Work(acf2s);
                    }else
                        ACF2s1 = acf2s.ToArray();

                    LoadList();
                }
            }
        }

        public void divideACF2_Work(List<String> acf2s)
        {
            int half = (acf2s.Count()-1)/2;
            List<String> firstHalf = acf2s.GetRange(0, half);
            ACF2s1 =  firstHalf.ToArray();

            acf2s.RemoveRange(0, half);

            ACF2s2 = acf2s.ToArray();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings form = new Settings(server,safety);
            DialogResult result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                String value = form.get_Safety();
                if (value == "")
                    safety = 0;
                else
                    safety = Convert.ToInt32(value);

                if(scope != null)
                    scope.Close();

                server = form.get_Server();
                scope = Connect("\\"+server);

                exportRejection_List = form.export_Rejection();
                exportNode_List = form.export_NodeList();
                exportSelection_Log = form.export_Logging();
                
                form.Dispose();
            }
            else
            {
                form.Dispose();
            }
        }
    }
}
