using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calculator_project
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
            My_Init();

  
        }
        public string userentereddigit;
        public string label1 = "";          // including input string, shown on monitor
        public string label2 = "";          // Result "string" , shown finally after "=" on monitor
        public string inputstring = "";     // first magnitude (string type) user enters via clicking or keyboard
        public string inputstring2 = "";    // second magnitude (string type) user enters via clicking or keyboard
        public double operand1 = 0;         //first magnitude : double
        public double operand2 = 0;         //second magnitude : double
        public double result = 0;           //the result is operand1 "operator" operand2 (23.4*8.4)
        int Errordot1 = 0;                  //check if we have entered "dot" in first operand. then disable dotbutton
        int Errordot2 = 0;                  //check if we have entered "dot" in second operand. then disable dotbutton
        public string Operator = "";        // for specifying the operator user wants
        public string Function = "";        // +-*/ & = is stored in this function
        public byte State = 1;              // 0: clear state    1: entering operand_1     2:pressing (+-/*)    3:entering operand_2   4: pressing =
        public int count_posneg = 0;        // pos-neg button: if pressed odd times--> negative , if pressed even times--> possitive 
        public string PosNeg_sign = "";
        public string signed_inputstring;
        public string signed_inputstring2;
        private void dOT_DISABLE()              //this function disables dot in order to forbid multi-dot in an operan 8.55.98! is Not Valid!
        {
            buttondot.Enabled = false;
        }
        private void dOT_ENABLE()               //this function Enables dot when yet no dot is entered.
        {
            buttondot.Enabled = true;
        }

        private void oPERATOR_Disable()      // disables operator keys ( *+-/) ( 8*-(!!!)938)
        {
            Mul.Enabled = false;
            Add.Enabled = false;
            Sub.Enabled = false;
            Div.Enabled = false;
        }

        private void oPERATOR_Enable()      // Enables operator keys after first operand is starting to be entered by user
        {
            Mul.Enabled = true;
            Add.Enabled = true;
            Sub.Enabled = true;
            Div.Enabled = true;
            Result.Enabled = false;
        }

         public void pOS_nEGsignfunc (string s)
        {
            if (PosNeg_sign == "-")
            {
                if (State == 1)
                {
                    signed_inputstring = PosNeg_sign + inputstring;
                    label1 =  signed_inputstring ;
                    Monitor.Text = label1;
                }
                if (State == 3)
                {
                    signed_inputstring2 = PosNeg_sign + inputstring2;
                    label1 = signed_inputstring + Operator +  signed_inputstring2 ;
                    Monitor.Text = label1;
                }
            }
            if (PosNeg_sign == "")
            {
                if (State == 1)
                {
                    signed_inputstring = PosNeg_sign + inputstring;
                    label1 = signed_inputstring;
                    Monitor.Text = label1;
                }
                if (State == 3)
                {
                    signed_inputstring2 = PosNeg_sign + inputstring2;
                    label1 = signed_inputstring + Operator +  signed_inputstring2 ;
                    Monitor.Text = label1;
                }
            }
            
            
        }
        public void oPERAND_MANAGERDETECT(string s)     //when a digit(0 to 9 even . ) is entered this function by state as a input
        {
            Backspace.Enabled = true;                   // determines which operand is enterring or finished
            Clear.Enabled = true;                       // CE and C and Backspase should be accessable for user so enable them
            Reset.Enabled = true;
             if (State == 1)
            {
                oPERAND1_MANAGER(Function);             //inputting first operand
            }
             if (State == 2)
             {
                 oPERAND1_FINISHED(Function);              // operator was entered so first operand is finished
             }
            if(State==3)
            { 
                oPERAND2_MANAGER(Function);                 // inputting second operand
            }

            if (State == 4)
            {
                oPERAND2_FINISHED(Function);                // "=" is entered second is finished 
            }
        }

        public void oPERATOR_MANAGERDETECT(string s)            // entered function is operator or = ? 
        {
            Clear.Enabled = true;
            Reset.Enabled = true;
            switch (Function)
            {
                case "+":
                    {
                        State = 2;
                        oPERAND1_FINISHED(Function);
                        break;

                    }
                case "-":
                    {
                        State = 2;
                        oPERAND1_FINISHED(Function);
                        break;
                    }
                case "*":
                    {
                        State = 2;
                        oPERAND1_FINISHED(Function);
                        break;
                    }
                case "/":
                    {
                        State = 2;
                        oPERAND1_FINISHED(Function);
                        break;
                    }
                case "=":
                    {
                        State = 4;
                        oPERAND2_FINISHED(Function);
                        break;
                    }
            }
        }
             

                    //***         ***
         // when state = 1 getting first operand from user. 
        public void oPERAND1_MANAGER(string s)         
        {
            oPERATOR_Enable() ;                         // every moment operator enable to be entered

            if (Function == "")                             // continue storing in first operand
            {                                               // or going to next state
                inputstring += userentereddigit;
                signed_inputstring = PosNeg_sign + inputstring;
            }
            else
            {
                oPERAND1_FINISHED(Function);
            }

            if (Errordot1 == 1)                         // here checks if before dot was entered or not
            {
                dOT_DISABLE();
            }
            else
                dOT_ENABLE();
        }

                 //   ****            ****
        // it is called whenever stat=2 then go to operand2-manager
        public double oPERAND1_FINISHED(string s)           
        {
                       // State=2;
            State ++;
            Operator = Function;                            // now function!="" so assign function to operator
            
            if (inputstring == ".")
            {
                inputstring = "0";
                operand1 = 0;
            }
            else
            {
               
                operand1 = Convert.ToDouble(signed_inputstring); 
            }
            count_posneg = 0;
            PosNeg_sign = "";
            dOT_ENABLE();
            oPERATOR_Disable();

            return 0;
        }

                    //****            ****
        // it is called whenever state=3 and getting second operand
         public void oPERAND2_MANAGER(string s)         
        {
            //state=3
           // oPERATOR_Disable();     

            if (Function != "=")            // getting second operand till = is entered
            {
                inputstring2 += userentereddigit;
                signed_inputstring2 = PosNeg_sign + inputstring2;
            }
            else
            {
                oPERAND2_FINISHED(Function);
            }

            if (Errordot2 == 1)         // check dotbutton pressed during this function: enable or disable it
            {
                dOT_DISABLE();
            }
            else
                dOT_ENABLE();

            Result.Enabled = true;          //every moment user can press = button so enable it
        }

                    //***         ***
         // it is called whenever stat=4 AND = is pressed down : CALCULATE TIME :D
         public double oPERAND2_FINISHED(string s) 
        {
            //state=4
            if (inputstring2 == ".")
            {
               // label1 = inputstring+Operator+"0";
                operand2 = 0;
            }
            else
            {
                 operand2 = Convert.ToDouble(signed_inputstring2); 
            }
            count_posneg = 0;
            PosNeg_sign = "";
            calculate(operand1, operand2, Operator);
            return 0;
        }



         public double calculate(double op1, double op2, string func) //whenever = is pressed we start calculating :D
        {
            switch (Operator)           // this switch identifies which on 4 operator is press and operator1 & operator2 are given too :D
            {
                case "*":
                    {
                        result = operand1 * operand2;
                        break;
                    }

                case "+":
                    {
                        result = operand1 + operand2;
                        break;
                    }


                case "/":
                    {
                        result = operand1 / operand2;
                        break;
                    }


                case "-":
                    {
                        result = operand1 - operand2;
                        break;
                    }


            }

            label2 = result.ToString();             // these 2-lines : showing result on the monitor
            Monitor.Text = label2;

            inputstring = "";               // reset all variables  
            inputstring2 = "";
            operand1 = 0;                        
            operand2 = 0;
            result = 0;
            Function = "";
            Operator = "";
            label1 = "";
            label2 = "";
            dOT_ENABLE();
            Backspace.Enabled = false;
            State = 1;
            return 0;
        }


        /*** definition of input events user can do ***/
        private void label1_Click(object sender, EventArgs e)   
        {

        }

       

        private void buttondot_Click(object sender, EventArgs e)
        {
            if (State == 1)                 // if operand 1 or 2 are enterring by user errordot=1(for disabling dotbutton)
            {
                Errordot1 = 1;
            }
            if (State == 3)
            {
                Errordot2 = 1;
            }

            label1 += ".";
            userentereddigit = ".";
            Monitor.Text = label1;
            oPERAND_MANAGERDETECT(State.ToString()) ;
        }

       

        private void button0_Click(object sender, EventArgs e)
        {
            label1 += "0";
            userentereddigit = "0";
            Monitor.Text = label1;
            oPERAND_MANAGERDETECT(State.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1 += "1";
            userentereddigit = "1";
            Monitor.Text = label1;
            oPERAND_MANAGERDETECT(State.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1 += "2";
            userentereddigit = "2";
            Monitor.Text = label1;
            oPERAND_MANAGERDETECT(State.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1 += "3";
            userentereddigit = "3";
            Monitor.Text = label1;
            oPERAND_MANAGERDETECT(State.ToString());

        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1 += "4";
            userentereddigit = "4";
            Monitor.Text = label1;
            oPERAND_MANAGERDETECT(State.ToString());

        }

        private void button5_Click(object sender, EventArgs e)
        {
            label1 += "5";
            userentereddigit = "5";
            Monitor.Text = label1;
            oPERAND_MANAGERDETECT(State.ToString());

        }

        private void button6_Click(object sender, EventArgs e)
        {
            label1 += "6";
            userentereddigit = "6";
            Monitor.Text = label1;
            oPERAND_MANAGERDETECT(State.ToString());
        }


        private void button7_Click(object sender, EventArgs e)
        {
            label1 += "7";
            userentereddigit="7";
            Monitor.Text = label1;
            oPERAND_MANAGERDETECT(State.ToString());
        }


        private void button8_Click(object sender, EventArgs e)
        {
            label1 += "8";
            userentereddigit = "8";
            Monitor.Text = label1;
            oPERAND_MANAGERDETECT(State.ToString());

        }

        private void button9_Click(object sender, EventArgs e)
        {
            label1 += "9";
            userentereddigit = "9";
            Monitor.Text = label1;
            oPERAND_MANAGERDETECT(State.ToString());

        }
        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
            label1 += "+";
            Monitor.Text = label1;
            Function = "+";
            oPERATOR_MANAGERDETECT(Function);
            oPERATOR_Disable();
        }

        private void Result_Click(object sender, EventArgs e)
        {
            Function = "=";
            oPERATOR_MANAGERDETECT(Function);
            Result.Enabled = false;
        }

        private void Mul_Click(object sender, EventArgs e)
        {
           label1 += "*";
           Monitor.Text = label1;
           Function = "*";
           oPERATOR_MANAGERDETECT(Function);
        }

        private void Sub_Click(object sender, EventArgs e)
        {
                label1 += "-";
                Monitor.Text = label1;
                Function = "-";
                oPERATOR_MANAGERDETECT(Function);
         }


        private void Div_Click(object sender, EventArgs e)
        {

            label1 += "/";
            Monitor.Text = label1;
            Function = "/";
            oPERATOR_MANAGERDETECT(Function);
       }


       

        private void Calculator_Load(object sender, EventArgs e)
        {

        }

       

        
       
        private void Clear_Click(object sender, EventArgs e)                // checking state and clear just the operand we are inputting
        {   
            if (State == 1)                     
            {
                inputstring = "";
                operand1 = 0;
                label1 = "";
                Errordot1 = 0;
            }
            if (State == 3)
            {
                inputstring2 = "";
                label1 = inputstring + Operator + "";
                Errordot2 = 0;
            }

            oPERATOR_Disable();                         // some required initialization
            dOT_ENABLE();
            Monitor.Text = label1;
            Result.Enabled = false;
            Clear.Enabled = false;
            count_posneg = 0;
            PosNeg_sign = "";
            
        }

        private void Reset_Click(object sender, EventArgs e)            // reset all variables
        {
            label1 = "";
            label2 = "";
            inputstring = "";
            inputstring2 = "";
            Operator = "";
            operand1 = 0;     
            operand2 = 0;
            Errordot1 = 0;
            Errordot2 = 0;
            Function = "";
            result = 0;       
            Monitor.Text = "0";
            State=1;
            oPERATOR_Disable();
            dOT_ENABLE();
            Result.Enabled = false;
            Reset.Enabled = false;
            Clear.Enabled = false;
            Backspace.Enabled = true;
            count_posneg = 0;
            PosNeg_sign = "";

        }

      

        private void My_Init()
        {
            oPERATOR_Disable();
            
            Result.Enabled = false; 
            
        }

        private void button10_Click(object sender, EventArgs e)             //Backspace button deletes last char and updates variables. 
        {
            switch(State)                                                   //just deletes last char of each string(digit) uses's entered
            {                                                               //and disables when mentioned operand is cleared completely
                case 1:                                                     // notice that operators can not be deleted
                    {
                        if (inputstring.Length > 0) //if operand-1 is enterring check if deleted char is "." or not. if dot--> errordot=0( enable dot)
                        {
                            if (inputstring.EndsWith("."))
                            {
                                Errordot1 = 0;
                                dOT_ENABLE();
                            }
                                inputstring = inputstring.Substring(0, inputstring.Length - 1);         // delete last char
                                label1 = label1.Substring(0, label1.Length - 1);                        //update monitor and input string
                                Monitor.Text = label1;
                            
                        }
                        else
                        {                           // if string length is 0 monitor and that variable =0                
                            inputstring = "";
                            label1 = "";
                            Monitor.Text = "0";
                            Errordot1 = 0;
                        }
                        break;
                    }
                case 2:
                    {
                        Backspace.Enabled = false;
                        break;
                    }
                case 3:
                    {   
                        if (inputstring2 != "")             // operand-2: if inputstirng-2 is not null chek if the last char ( deleted one) is "." 
                        {                                   //or not. initialize proper errordot-2 to enable or disable "."
                            if (inputstring2.EndsWith("."))
                            {
                                Errordot2 = 0;
                                dOT_ENABLE();
                            }

                                inputstring2 = inputstring2.Substring(0, inputstring2.Length - 1);      // remove last char 
                                label1 = label1.Substring(0, label1.Length - 1);                        // update monitor and variable(operand2)
                                Monitor.Text = label1;
                                Backspace.Enabled = true;
                            
                        }
                        else
                        {                                       // if current amp. is null then update monitor by operand1+operator+null(oeprand2)
                            label1 = inputstring + Operator;
                            Backspace.Enabled = false;
                        }

                        break;
                    }
                case 4:
                    {
                        Backspace.Enabled = false;
                        break;
                    }

            }   
        }

        private void PosNeg_Click(object sender, EventArgs e)
        {
            count_posneg++;
            if (count_posneg % 2 == 0)
            { PosNeg_sign = "";
            pOS_nEGsignfunc(PosNeg_sign);
            }
            else 
            {
                PosNeg_sign = "-";
                pOS_nEGsignfunc(PosNeg_sign);
                /*if (State == 1)
                {
                    inputstring = PosNeg_sign + inputstring;
                    label1 = PosNeg_sign + label1;
                    Monitor.Text = label1;
                }
                if (State == 3)
                {
                    inputstring2 = PosNeg_sign + inputstring2;
                    label1 = inputstring + Operator + "(" + inputstring2 + ")";
                    Monitor.Text = label1;
                }*/
            }
        }


        private void Calculator_KeyPress(object sender, KeyPressEventArgs e)
        {

            
            string s;
            s=e.KeyChar.ToString();
            //MessageBox.Show(s);
            //if (e.KeyChar.ToString()==(char)27)
            //{
           // string Esc =e.KeyChar.ToString();
            //}

            switch (s)
            {
                case "0":
                    {
                        label1 += "0";
                        userentereddigit = "0";
                        Monitor.Text = label1;
                        oPERAND_MANAGERDETECT(State.ToString());

                        break;
                    }
                case "1":
                    {
                        label1 += "1";
                        userentereddigit = "1";
                        Monitor.Text = label1;
                        oPERAND_MANAGERDETECT(State.ToString());

                        break;
                    }
                case "2":
                    {
                        label1 += "2";
                        userentereddigit = "2";
                        Monitor.Text = label1;
                        oPERAND_MANAGERDETECT(State.ToString());

                        break;
                    }
                case "3":
                    {
                        label1 += "3";
                        userentereddigit = "3";
                        Monitor.Text = label1;
                        oPERAND_MANAGERDETECT(State.ToString());

                        break;
                    }
                case "4":
                    {
                        label1 += "4";
                        userentereddigit = "4";
                        Monitor.Text = label1;
                        oPERAND_MANAGERDETECT(State.ToString());

                        break;
                    }
                case "5":
                    {
                        label1 += "5";
                        userentereddigit = "5";
                        Monitor.Text = label1;
                        oPERAND_MANAGERDETECT(State.ToString());

                        break;
                    }
                case "6":
                    {
                        label1 += "6";
                        userentereddigit = "6";
                        Monitor.Text = label1;
                        oPERAND_MANAGERDETECT(State.ToString());

                        break;
                    }
                case "7":
                    {
                        label1 += "7";
                        userentereddigit = "7";
                        Monitor.Text = label1;
                        oPERAND_MANAGERDETECT(State.ToString());

                        break;
                    }
                case "8":
                    {
                        label1 += "8";
                        userentereddigit = "8";
                        Monitor.Text = label1;
                        oPERAND_MANAGERDETECT(State.ToString());

                        break;
                    }
                case "9":
                    {
                        label1 += "9";
                        userentereddigit = "9";
                        Monitor.Text = label1;
                        oPERAND_MANAGERDETECT(State.ToString());

                        break;
                    }
                case ".":
                    {
                        if (State == 1)
                        {
                            if (Errordot1 == 1)
                            {
                                break;
                            }
                            Errordot1 = 1;
                        }
                        if (State == 3)
                        {
                            if (Errordot2 == 1)
                            {
                                break; 
                            }
                            Errordot2 = 1;
                        }
                        label1 += ".";
                        userentereddigit = ".";
                        Monitor.Text = label1;
                        // Function = ".";
                        oPERAND_MANAGERDETECT(State.ToString());
                        break;
                    }
                case "+": 
                {
                    if (Operator == "")
                    {
                        label1 += "+";
                        Monitor.Text = label1;
                        Function = "+";
                        oPERATOR_MANAGERDETECT(Function);
                    }
                   oPERATOR_Disable();
                   break;
                }
                case "-":
                {
                    if (Operator == "")
                    {
                        label1 += "-";
                        Monitor.Text = label1;
                        Function = "-";
                        oPERATOR_MANAGERDETECT(Function);
                    }
                    oPERATOR_Disable();
                    break;
                }
                case "*":
                {
                    if (Operator == "")
                    {
                        label1 += "*";
                        Monitor.Text = label1;
                        Function = "*";
                        oPERATOR_MANAGERDETECT(Function);
                    }
                    oPERATOR_Disable();
                    break;
                }
                case "/":
                {
                    if (Operator == "")
                    {
                        label1 += "/";
                        Monitor.Text = label1;
                        Function = "/";
                        oPERATOR_MANAGERDETECT(Function);
                    }
                    oPERATOR_Disable();
                    break;
                }

                case "=":
                {
                    if (inputstring2 != "")
                    {
                        Result.Enabled = true;
                        Function = "=";
                        oPERATOR_MANAGERDETECT(Function);
                        break;
                    }
                    else
                    { Result.Enabled = false; 
                    }
                    break;
                }
              
                
                case "`":               //clear
                {
                    if (State == 1)
                    {
                        //if (inputstring == "")
                        //{ break; }
                        inputstring = "";
                        operand1 = 0;
                        label1 = "";
                        Errordot1 = 0;
                    }
                    if (State == 3)
                    {
                        if (inputstring2 == "") 
                        { break; }
                        inputstring2 = "";
                        label1 = inputstring + Operator + "";
                        Errordot2 = 0;
                    }

                    oPERATOR_Disable();
                    dOT_ENABLE();
                    // new digit which should save on Sum1 to clear
                    Monitor.Text = label1;
                    Result.Enabled = false;
                    Clear.Enabled = false;
                    count_posneg = 0;
                    PosNeg_sign = "";
                    break;
 
                }
                case "c" :                    //reset
                {  
                    label1 = "";
                    label2 = "";
                    inputstring = "";
                    inputstring2 = "";
                    Operator = "";
                    operand1 = 0;     // reset all stacks
                    operand2 = 0;
                    Errordot1 = 0;
                    Errordot2 = 0;
                    Function = "";
                    result = 0;       // reset all stacks
                    Monitor.Text = "0";
                    State = 1;
                    oPERATOR_Disable();
                    dOT_ENABLE();
                    Result.Enabled = false;
                    Reset.Enabled = false;
                    Clear.Enabled = false;
                    Backspace.Enabled = true;
                    count_posneg = 0;
                    PosNeg_sign = "";
                    break;
                }

                case "\b":                    // backspace
                {
                    switch (State)
                    {
                        case 1:
                            {
                                if (inputstring.Length > 0)
                                {
                                    if (inputstring.EndsWith("."))
                                    {
                                        Errordot1 = 0;
                                        dOT_ENABLE();
                                    }
                                    inputstring = inputstring.Substring(0, inputstring.Length - 1);
                                    label1 = label1.Substring(0, label1.Length - 1);
                                    Monitor.Text = label1;

                                }
                                else
                                {
                                    inputstring = "";
                                    label1 = "";
                                    Monitor.Text = "0";
                                    Errordot1 = 0;
                                }
                                break;
                            }
                        case 2:
                            {
                                /*label1 = label1.Substring(0, label1.Length - 1);
                                Monitor.Text = label1;
                                Function = "";
                                Operator = "";
                                oPERATOR_Enable();
                                State--;*/
                                Backspace.Enabled = false;
                                break;
                            }
                        case 3:
                            {
                                if (inputstring2 != "")
                                {
                                    if (inputstring2.EndsWith("."))
                                    {
                                        Errordot2 = 0;
                                        dOT_ENABLE();
                                    }

                                    inputstring2 = inputstring2.Substring(0, inputstring2.Length - 1);
                                    label1 = label1.Substring(0, label1.Length - 1);
                                    Monitor.Text = label1;
                                    Backspace.Enabled = true;

                                }
                                else
                                {
                                    label1 = inputstring + Operator;
                                    Backspace.Enabled = false;
                                }

                                break;
                            }
                        case 4:
                            {
                                Backspace.Enabled = false;
                                break;
                            }

                    }
                    break;
                }


            
            }
        }

       





    }
    
    
    
    
}
