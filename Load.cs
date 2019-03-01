using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtkilesimDiyagram
{
    public class Load: INotifyPropertyChanged, ICloneable
    {
        public double M { get; set; }
        public double N { get; set; }

        public Load()
        {

        }

        public Load(double M, double N)
        {
            this.M = M;
            this.N = N;
        }

        public Load(Load lo)
        {
            M = lo.M;
            N = lo.N;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public object Clone()
        {
            return new Load(this);
        }
    }
}
