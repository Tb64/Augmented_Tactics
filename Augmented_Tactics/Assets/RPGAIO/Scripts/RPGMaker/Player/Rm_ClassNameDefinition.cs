using System;
using LogicSpawn.RPGMaker.Core;

namespace LogicSpawn.RPGMaker
{
    public class Rm_ClassNameDefinition
    {
        public string ID;
        public string Name;
        public ImageContainer Image;

        public Rm_ClassNameDefinition()
        {
            ID = Guid.NewGuid().ToString();
            Image = new ImageContainer();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}