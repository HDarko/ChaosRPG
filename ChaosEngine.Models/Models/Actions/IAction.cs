using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Models.Actions
{
    public interface IAction
    {
        event EventHandler<string> OnActionPerformed;
        void Execute(LivingEntity actor, LivingEntity target);
    }
}
