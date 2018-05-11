using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDepartment
{
    class Requests
    {
        public int number;
        public bool isRush;
        public bool isProcessing;
        public Designer whatStage;
        public int processingTime;

        public Requests(int number, bool isRush, bool isProcessing, Designer whatStage, int processingTime) {
            this.number = number;
            this.isRush = isRush;
            this.isProcessing = isProcessing;
            this.whatStage = whatStage;
            this.processingTime = processingTime;
        }

        public int getNumber() {
            return number;
        }

        public void setNumber(int number) {
            this.number = number;        
        }

        public bool getIsRush()
        {
            return isRush;
        }

        public void setIsRush(bool isRush)
        {
            this.isRush = isRush;
        }

        public bool getIsProcessing()
        {
            return isProcessing;
        }

        public void setIsProcessing(bool isProcessing)
        {
            this.isProcessing = isProcessing;
        }

    }
}
