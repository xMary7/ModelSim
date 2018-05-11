using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectDepartment
{
    class Program
    {
        static Designer[] Designers;
       // static Requests[] Requests;
        static Random r = new Random();
        //static int numOfTasks = 10;
        //static int numOfRushTasks = 2;
        //static int[] mas = new int[numOfRushTasks];
        //static int a = r.Next(5), b = r.Next(a - 1), c, d;
        static int allTime = 0;
        static int localTime = 0;
        
        
        static void Main(string[] args)
        {
            int numOfTasks = 0, numOfRushTasks = 0;
            int nStages = 4;
            InputNumberOfTasks(ref numOfTasks, ref numOfRushTasks);
            Console.WriteLine("All tasks - {0}, rush tasks - {1}.", numOfTasks, numOfRushTasks);
            Designer[] Designers = workingTime(nStages);
            Requests[] Requests = createOrder(numOfTasks, numOfRushTasks, Designers);            
            Dictionary<Requests, int> distribution = Distribution(Requests, numOfTasks);

            //int cntTasks = 0;
            //int cntStages = 0;
            int days = 0;
            int temp = distribution[Requests[0]];
            int i = 0, j = 0, k = 0, l = 0;
            int[] cntRush = new int[numOfRushTasks];
            for (int cnt = 0; cnt < numOfRushTasks; cnt++) {
                cntRush[cnt] = 0;
            }
            int[] progTime = new int[numOfTasks];
            //Console.WriteLine("Requests[numOfTasks - 1].whatStage.number = {0}", Requests[numOfTasks - 1].whatStage.number);
            while ((Requests[numOfTasks - 1].whatStage.number != nStages) && (Requests[numOfTasks - 1].isProcessing != true)) { 
                //progTime[cntStages] = distribution[Requests[cntTasks]];
                days++;
                if (i < numOfTasks)
                {
                    if (temp <= days)
                    {
                        if ( ((temp + distribution[Requests[i + 1 + cntRush[0]]]) == days) && (Requests[i + 1 + cntRush[0]].isRush == true) && (Requests[i].isRush == false)) {
                            i = i + 1 + cntRush[0];
                            cntRush[0]++;
                            temp += distribution[Requests[i]];
                            Designers[0].ev = Events.DoingRush;
                        }

                        if ((Designers[0].ev != Events.TaskProcessing) || (Designers[0].ev == Events.DoingRush))
                        {
                            Requests[i].whatStage.number = Designers[0].number;
                            Requests[i].processingTime = Designers[0].workTime;
                            Requests[i].isProcessing = true;
                            Designers[0].ev = Events.TaskProcessing;
                        }
                        Requests[i].processingTime--;

                        if (Requests[i].processingTime == 0)
                        {
                            Requests[i].isProcessing = false;
                            Designers[0].ev = Events.TaskReadiness;
                            
                            if (Requests[i].isRush == true)
                            {
                                Designers[1].ev = Events.DoingRush;
                                while ((Requests[i].isRush == true) && (i > 1)) 
                                {
                                    i = i - 1;
                                }
                                Designers[0].ev = Events.TaskProcessing;
                            }
                            else
                            {
                                i = i + 1 + cntRush[0];
                                if (cntRush[0] > 0) {
                                    cntRush[0] = 0;
                                }
                               
                                if (i < numOfTasks)
                                {
                                    temp += distribution[Requests[i]];
                                }
                            }

                        }
                    }
                }
                else {
                    Designers[0].ev = Events.TaskWaiting;
                }


                if (((((Designers[0].ev == Events.TaskWaiting) || (Designers[0].ev == Events.TaskProcessing)) && (i > j)) || Designers[1].ev == Events.DoingRush) && (j < numOfTasks))
                {
                    if ((Designers[1].ev == Events.DoingRush) && (Requests[j + 1 + cntRush[1]].isRush == true) && (Requests[j].isRush == false))
                    {
                            j = j + 1 + cntRush[1];
                            cntRush[1]++;
                     
                    }
                    if (Designers[1].ev != Events.TaskProcessing)
                    {
                        Requests[j].whatStage.number = Designers[1].number;
                        Requests[j].processingTime = Designers[1].workTime;
                        Requests[j].isProcessing = true;
                        Designers[1].ev = Events.TaskProcessing;
                    }
                    Requests[j].processingTime--;

                    if (Requests[j].processingTime == 0)
                    {
                        Requests[j].isProcessing = false;
                        Designers[1].ev = Events.TaskReadiness;
                        j++;
                    }
                }
                else {
                    Designers[1].ev = Events.TaskWaiting;
                }
                if (((Designers[1].ev == Events.TaskWaiting) || (Designers[1].ev == Events.TaskProcessing)) && (j > k) && (k < numOfTasks))
                {
                    if (Designers[2].ev != Events.TaskProcessing)
                    {
                        Requests[k].whatStage.number = Designers[2].number;
                        Requests[k].processingTime = Designers[2].workTime;
                        Requests[k].isProcessing = true;
                        Designers[2].ev = Events.TaskProcessing;
                    }
                    Requests[k].processingTime--;
                    
                    if (Requests[k].processingTime == 0)
                    {
                        Requests[k].isProcessing = false;
                        Designers[2].ev = Events.TaskReadiness;
                        k++;
                    }
                }
                else
                {
                    Designers[2].ev = Events.TaskWaiting;
                }
                if (((Designers[2].ev == Events.TaskWaiting) || (Designers[2].ev == Events.TaskProcessing)) && (k > l) && (l < numOfTasks))
                {
                    if (Designers[3].ev != Events.TaskProcessing)
                    {
                        Requests[l].whatStage.number = Designers[3].number;
                        Requests[l].processingTime = Designers[3].workTime;
                        Requests[l].isProcessing = true;
                        Designers[3].ev = Events.TaskProcessing;
                    }
                    Requests[l].processingTime--;
                    
                    if (Requests[l].processingTime == 0)
                    {
                        Requests[l].isProcessing = false;
                        Designers[3].ev = Events.TaskReadiness;
                        l++;
                    }
                }
                else
                {
                    Designers[3].ev = Events.TaskWaiting;
                }
            }
           // withoutDuplication(numOfTasks, numOfRushTasks);
           /* for (int i = 0; i < numOfTasks; i++)
            {

                // localTime = r.Next(a - b, a + b);
                for (int j = 0; j < nStages; j++)
                {
                    if (Designers[j].status == 0)
                    {
                        Designers[j].status = 1;
                    }
                    allTime += Designers[j].workTime;
                }
            }*/
            }

        
    public static Dictionary<Requests, int> Distribution(Requests[] Requests, int size) {
        int paramA, paramB;

        Dictionary<Requests, int> dictionary = new Dictionary<Requests,int>();
        for (int i = 0; i < size; i++) {
            paramA = r.Next(1, 3);
            paramB = r.Next(paramA - 1);
            dictionary[Requests[i]] = r.Next(paramA - paramB, paramA + paramB);
        }
        return dictionary;
    }


            public static void InputNumberOfTasks(ref int numOfTasks, ref int numOfRushTasks)
            {
                string number;
                Console.WriteLine("Введите число заявок: ");
                number = Console.ReadLine();
                numOfTasks = Convert.ToInt32(number);

                Console.WriteLine("Введите число срочных заявок: ");
                number = Console.ReadLine();
                numOfRushTasks = Convert.ToInt32(number);

            }


            public static Requests[] createOrder(int numOfTasks, int numOfRushTasks, Designer[] Designers)
            {
                int cnt = 0;
                int[] mas = new int[numOfTasks];
                Requests[] Requests = new Requests[numOfTasks];
                for (int i = 0; i < numOfTasks; i++)
                {
                    Requests[i] = new Requests(i, false, false, Designers[0], 0);   
                   
                }

                while (cnt < numOfRushTasks) {
                    for (int i = 0; i < numOfTasks; i++) {
                        if (cnt < numOfRushTasks) {
                            if (Requests[i].isRush == false)
                            {
                                Requests[i].isRush = Convert.ToBoolean(r.Next(0, 2));
                                if (Requests[i].isRush != false)
                                {
                                    cnt++;
                                }
                            }
                        }
                        else {
                            break;
                        }
                    }
                }

                for (int i = 0; i < numOfTasks; i++) {
                    Console.WriteLine("{0} - {1}", i, Requests[i].isRush);
                }

                return Requests;
            }

            public static Designer[] workingTime(int nStages) {
                Designer[] Designers = new Designer[nStages];
                int paramC, paramD, time;
                for (int i = 0; i < nStages; i++) {
                    paramC = r.Next(1, 7);
                    paramD = r.Next(paramC - 1);
                    time = r.Next(paramC - paramD, paramC + paramD);
                    Designers[i] = new Designer(i, time, 0, Events.TaskEntrance);                
                }
                return Designers;
            }

            public static void printRequests(Requests[] Requests, int size) {
                for (int i = 0; i < size; i++)
                {
                    //Console.WriteLine("{0} - {1}", i, mas[i]);
                    Console.WriteLine("{0} - {1}", i, Requests[i].isRush);
                }
            }
        }


       /* public static int[] withoutDuplication(int numOfTasks, int numOfRushTasks) {
            int[] mas = new int[numOfRushTasks];
            // int temp;
            for (int i = 0; i < numOfRushTasks; i++)
            {
                mas[i] = r.Next(numOfTasks);
            }
            mas = rushTasks(mas, numOfTasks, numOfRushTasks);

            return mas;
        
        }

        public static int[] rushTasks(int[] mas, int numOfTasks, int numOfRushTasks) {
            int m = 0;
            for (int i = 0; i < numOfRushTasks; i++) {
                for (int j = 0; j < numOfRushTasks; j++) {
                    m = 0;
                    if ((mas[i] == mas[j]) && (i != j) && (mas[i] != 0))
                    {
                        m = 1;
                        mas[j] = 0;


                    }
                }
            }
            for (int i = 0; i < numOfRushTasks; i++) {
                if (mas[i] == 0) {
                   mas[i] = r.Next(numOfTasks);
                    }
                    
            }
            while (m == 1) {
                mas = rushTasks(mas, numOfTasks, numOfRushTasks);
            }
                
                return mas;
        }
    }*/
}
