using System.Collections.Generic;
using UnityEngine;

namespace DataStruct
{
    [System.Serializable]
    //items 
    public struct Inventory
    {
        public bool[] cells;
        public ItemType[] item;
    }
    //info about items
    public struct ItemsInfo
    {
        public string[] itemsInfo;
    }
    //data that have act and task for gameplay
    public struct ActLevel
    {
        public int act;
        public int task;
    }
    //info about act and tasks
    public struct ActTask
    {
        public string[] firstAct;
        public string[] secondAct;
        public string[] thirdAct;
        public string[] fourthAct;
        public string[] fifthAct;

    }
}