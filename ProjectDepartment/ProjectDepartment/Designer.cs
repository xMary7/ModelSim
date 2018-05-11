using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDepartment
{
    class Designer
    {
        public int number;
        public int workTime;
        public int status;
        public Events ev;

        public Designer(int number, int workTime, int status, Events ev) {
            this.number = number;
            this.workTime = workTime;
            this.status = status;
            this.ev = ev;
        }

        public int getNumber() {
            return number;
        }

        public void setNumber(int number) {
            this.number = number;
        }

        public int getWorkTime()
        {
            return workTime;
        }

        public void setWorkTime(int workTime)
        {
            this.workTime = workTime;
        }

        public int getStatus()
        {
            return status;
        }

        public void setStatus(int status)
        {
            this.status = status;
        }
    }

}
