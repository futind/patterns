using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMtest
{
    internal interface ICommand
    {
        void Execute();
    }

    public abstract class ACommand: ICommand
    {
        public void Execute() { 
            CM.GetInstance().Registry(this);
            this.doExecute();
        }
        public abstract void doExecute();
    }

    public class CM
    {
        private CM() { }

        private static CM instance = null;

        public static CM GetInstance()
        {
            if (instance == null)
                instance = new CM();
            return instance;
        }

        private bool LOCK = false;
        private List<ICommand> list = new List<ICommand>();

        public void Registry(ACommand c)
        {
            if (LOCK) return;  
            list.Add(c);
        }
        public void Undo()
        {
            if (list.Count == 0) return;
            LOCK = true;
            list.RemoveAt(list.Count - 1);
            foreach(var c in list)
                c.Execute();
            LOCK = false;
        }
    }
}
