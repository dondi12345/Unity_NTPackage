using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NTPackage.Functions
{
    public class TestVerticalScroll : VerticalScroll
    {
        public List<int> List;

        void Start()
        {
            this.List = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                this.List.Add(i);
            }
            this.OnUI(this.List.Count);
        }
    }
}
