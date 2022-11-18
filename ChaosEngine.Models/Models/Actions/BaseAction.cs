using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Models.Actions
{
    public abstract class BaseAction
    {
        private readonly GameItem _item;

        public event EventHandler<string> OnActionPerformed;
        protected void ReportResult(string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }

        protected BaseAction(GameItem itemInUse)
        {
            _item = itemInUse;
        }

    }
}
