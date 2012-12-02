using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RolloutLister
{
    class Sorter
    {

        public KeyValuePair< List<Main.NodeItem>,String> selectBest(List<Main.NodeItem> list,String owner,int safety, string logs)
        {
            logs += "Processing ACF2 : " + owner + "\n";
            List<Main.NodeItem> newList = new List<Main.NodeItem>(list);
            //Only 1 node is found so we should keep it and leave
            if (newList.Count <= safety)
            {
                logs += "Selection list has hit safety limit of '" + safety + "'\n";
                return new KeyValuePair< List<Main.NodeItem>,String> (newList,logs);
            }

            
            //Perform selections-------------------

            //Check if all nodes are virtual machines or all xp
            bool virt = true;
            bool xp = true;
            foreach (Main.NodeItem item in newList)
            {
                if (item.Node[2] != 'V')
                    virt = false;
                if(item.OS.Contains("6.1"))
                    xp = false;
                
            }
            int index = 0;
            

            //Find the newest created machine date, hopefully a desktop or laptop
            newList = sortByDescendingCreation(newList);
            DateTime newest_Creation = Convert.ToDateTime(newList[0].DateCreated).Date;
            foreach (Main.NodeItem item in newList)
            {
                if (item.Node[2] == 'N' || item.Node[2] == 'D')
                {
                    newest_Creation = Convert.ToDateTime(newList[0].DateCreated).Date;
                    break;
                }
            }

            //Sort by latest HeartBeat
            newList = sortByDescendingDate(newList);

            //Get the most recent heartbeats
            DateTime most_Recent = Convert.ToDateTime(newList[0].HeartBeat).Date;
            int second_Recent_Index = 0;
            foreach (Main.NodeItem item in newList)
            {
                if (item.Node[2] == 'N' || item.Node[2] == 'D')
                {
                    most_Recent = Convert.ToDateTime(newList[0].HeartBeat).Date;
                    break;
                }
            }

            
            DateTime second_Recent = most_Recent.Date.AddDays(-7);

            
            foreach (Main.NodeItem item in newList)
            {
               //Console.WriteLine("Comparing  " + item.HeartBeat);
                if (Convert.ToDateTime(item.HeartBeat).Date.CompareTo(second_Recent) <= 0)
                {
                    //second_Recent = Convert.ToDateTime(item.HeartBeat);
                    //Console.WriteLine("Second Recent is "+second_Recent);
                    break;
                }
                second_Recent_Index++;
            }

            //Collect only the most recent heartbeats
            index = 0;
            //Console.WriteLine("Count of new list is " + newList.Count);
            while (index < newList.Count)
            {
                if (newList.Count <= safety)
                {
                    logs += "Selection list has hit safety limit of '" + safety + "'\n";
                    return new KeyValuePair<List<Main.NodeItem>, String>(newList, logs);
                }

                if (Convert.ToDateTime(second_Recent).Date.CompareTo
                    (Convert.ToDateTime(newList[index].HeartBeat).Date) > 0)
                {
                    //Console.WriteLine("Removing non recent days " + newList[index].Node);
                    logs += "Removing node " + newList[index].Node + "-- Reason = Old Heartbeat\n";
                    newList.RemoveAt(index);
                }
                else
                    index++;
            }

            //Remove any nodes that the heartbeat is before the most recent creation date
            index = second_Recent_Index;
            //Console.WriteLine("List count is " + newList.Count);
            while (index < newList.Count)
            {
                if (newList.Count <= safety)
                {
                    logs += "Selection list has hit safety limit of '" + safety + "'\n";
                    return new KeyValuePair<List<Main.NodeItem>, String>(newList, logs);
                }

                if (Convert.ToDateTime(newest_Creation).Date.AddDays(6).CompareTo (Convert.ToDateTime(newList[index].HeartBeat).Date) >= 0)
                {
                    //Console.WriteLine("Removing before heartbeat machines " + newList[index].Node);
                    logs += "Removing node " + newList[index].Node + "-- Reason = Recent Heartbeat was too close to creation date. This node was likely replaced.\n";
                    newList.RemoveAt(index);
                }
                else
                    index++;
            }


            index = 0;
            //If there are win7 users it is likely that the primary machine is win7. Remove all winxp machines
            if (xp == false)
            {
                index = 0;
                while (index < newList.Count)
                {
                    if (newList.Count <= safety)
                    {
                        logs += "Selection list has hit safety limit of '" + safety + "'\n";
                        return new KeyValuePair<List<Main.NodeItem>, String>(newList, logs);
                    }

                    if (newList[index].OS.Contains("5.1"))
                    {
                        //Console.WriteLine("Removing XP " + newList[index].Node);
                        logs += "Removing node " + newList[index].Node + "-- Reason = Win7 machines were found. XP Machines will be replaced\n";
                        newList.RemoveAt(index);
                    }
                    else
                        index++;
                }
            }

            //Remove Non-Owners
            index = 0;
            newList = sortByAscendingDate(newList);
            while (index < newList.Count)
            {
                if (newList.Count <= safety)
                {
                    logs += "Selection list has hit safety limit of '" + safety + "'\n";
                    return new KeyValuePair<List<Main.NodeItem>, String>(newList, logs);
                }

                if (newList[index].OS.Contains("6.1"))
                {
                    if (newList[index].Owner.ToLower() != owner.ToLower() && newList[index].Owner.ToLower() != "Prestage".ToLower())
                    {
                        //Console.WriteLine("Removing at Non-Owners " + newList[index].Node + " Owner is :" + newList[index].Owner.ToLower());
                        logs += "Removing node " + newList[index].Node + "-- Reason = Win7 node owner did not match.\n";
                        newList.RemoveAt(index);
                    }
                    else if (newList[index].Active.Contains("0"))
                    {
                        logs += "Removing node " + newList[index].Node + "-- Reason = Node is no longer active\n";
                        newList.RemoveAt(index);
                    }
                    else
                        index++;
                }
                else
                {
                    if (newList[index].Owner.ToLower() != owner.ToLower() && newList[index].Owner.ToLower() != "")
                    {
                        //Console.WriteLine("Removing at Non-Owners " + newList[index].Node);
                        logs += "Removing node " + newList[index].Node + "-- Reason = Owner did not match.\n";
                        newList.RemoveAt(index);
                    }
                    else
                        index++;
                }

            }


            //Remove nodes that would potentially be a test machine by checking the 3rd character for 'B' or 'C'
            //This should be set by OSS as best practice.
            index = 0;
            while (index < newList.Count)
            {
                if (newList.Count <= safety)
                {
                    logs += "Selection list has hit safety limit of '" + safety + "'\n";
                    return new KeyValuePair<List<Main.NodeItem>, String>(newList, logs);
                }

                if (newList[index].Node[3] == 'B' || newList[index].Node[2] == 'V' || newList[index].Node[3] == 'C')
                {
                    if (virt != true)//Checks if all nodes found are virtual
                    {
                        //Console.WriteLine("Removing Test machines " + newList[index].Node);
                        logs += "Removing node " + newList[index].Node + "-- Reason = Virtual machine or test node was found.\n";
                        newList.RemoveAt(index);
                    }
                    else
                    {
                        index++;
                    }
                }
                else
                    index++;
            }

            

            //Console.WriteLine();
            DateTime earliestCreation = new DateTime();
            newList = sortByAscendingCreation(newList);
            //Console.WriteLine("NewList Size is " + newList.Count);
            bool has_Laptop = false;

            if (newList.Count > 0)
            {
                

                earliestCreation = Convert.ToDateTime(newList[0].DateCreated);
                index = 0;
                while (index < newList.Count)
                {
                    if (newList.Count <= safety)
                    {
                        logs += "Selection list has hit safety limit of '" + safety + "'\n";
                        return new KeyValuePair<List<Main.NodeItem>, String>(newList, logs);
                    }

                    if (newList[index].Node[2] == 'N' && has_Laptop == false)
                    {
                        index++;
                        has_Laptop = true;
                    }
                    else if (Convert.ToDateTime(newList[index].DateCreated).CompareTo(earliestCreation) > 0)
                    {
                        //Console.WriteLine("Removing Test machines that are earlier " + newList[index].Node);
                        logs += "Removing node " + newList[index].Node + "-- Reason = Node below creation threshold.\n";
                        newList.RemoveAt(index);
                    }
                    else
                        index++;
                }
            }

            logs += "Done selecting nodes from: "+owner;

            return new KeyValuePair< List<Main.NodeItem>,String> (newList,logs);
        }

        public List<Main.NodeItem> removeDuplicateNodes(List<Main.NodeItem> list)
        {
            list = sortByAscendingNode(list);

            Int32 index = 0;
            while (index < list.Count - 1)
            {
                if (list[index].Node == list[index + 1].Node)
                    list.RemoveAt(index);
                else
                    index++;
            }

            return list;
        }

        public List<Main.ACF2> removeDuplicateACF2(List<Main.ACF2> list)
        {
            list = sortByDescendingACF2(list);

            int index = 0;
            while (index < list.Count - 1)
            {
                if (list[index].ID.ToLower() == list[index + 1].ID.ToLower())
                    list.RemoveAt(index);
                else
                    index++;
            }

            list = sortByHighestNodeCount(list);

            return list;
        }

        public List<Main.ACF2> sortByHighestNodeCount(List<Main.ACF2> list)
        {
            list.Sort((a, b) => b.Nodes.Count.CompareTo(a.Nodes.Count));
            return list;
        }

        public List<Main.ACF2> sortByDescendingACF2(List<Main.ACF2> list)
        {
            list.Sort((a, b) => a.ID.CompareTo(b.ID));
            return list;
        }

        public List<Main.NodeItem> sortByAscendingDate(List<Main.NodeItem> list)
        {
            list.Sort((a, b) => Convert.ToDateTime(a.HeartBeat).CompareTo(Convert.ToDateTime(b.HeartBeat)));
            return list;
        }

        public List<Main.NodeItem> sortByDescendingDate(List<Main.NodeItem> list)
        {
            list.Sort((a, b) => Convert.ToDateTime(b.HeartBeat).CompareTo(Convert.ToDateTime(a.HeartBeat)));
            return list;
        }

        public List<Main.NodeItem> sortByAscendingCreation(List<Main.NodeItem> list)
        {
            list.Sort((a, b) => Convert.ToDateTime(a.DateCreated).CompareTo(Convert.ToDateTime(b.DateCreated)));
            return list;
        }

        public List<Main.NodeItem> sortByDescendingCreation(List<Main.NodeItem> list)
        {
            list.Sort((a, b) => Convert.ToDateTime(b.DateCreated).CompareTo(Convert.ToDateTime(a.DateCreated)));
            return list;
        }

        public List<Main.NodeItem> sortByAscendingNode(List<Main.NodeItem> list)
        {
            list.Sort((a, b) => a.Node.CompareTo(b.Node));
            return list;
        }

        public List<Main.NodeItem> sortByDescendingNode(List<Main.NodeItem> list)
        {
            list.Sort((a, b) => b.Node.CompareTo(a.Node));
            return list;
        }

    }
}
