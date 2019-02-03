/*
Hannah Chang
CPSC 3200
Created: 01/27/19 - EDITED: 01/29/19, 01/30/19, 01/31/19, 02/01/19
iSeq.cs - C#

CLASS INVARIANTS:          
I've created a const int ERROR_HANDLING = -1 which returns -1 
to the program to indicate that the iSeq object is inactive. 
-1 is an invalid number in the program because the arithmetic 
sequence is positive. The pth query is compared to numQueries 
before we return the next int in the sequence. Program only 
accepts integers but all negative numbers inputed to be used 
as the initial number in the sequence will be changed to unsigned 
integers. The client can reset to the original value that was 
entered into the program (unsigned version) at any point in the
program.

INTERFACE INVARIANTS:       
The pth query toggles the object to on/off; p varies from iSeq 
object to iSeq object. 

IMPLEMENTATION INVARIANTS:  
I've created an uint variable 'numQueries' in order to keep track 
of the number of times that query() is called so that we can 
compare 'numQueries' to 'p' and see if numQueries is a multiple 
of p; where p represents the number of times query() is called 
until iSeq object is toggled off/on. I've created a constant 
arithmetic sequence in getSequence() where the sequence starts 
with currentNum (initialized to the starting num that was passed 
into the constructor and converted to uint) and adds SEQUENCE_ADDER 
(10) to itself. (i.e, 0, 10, 20, 30, 40, 50,...). p is initialized 
in the constructor from the unsigned version of the number that 
the user enters into the program. The first query will output 
the unsigned version of the number that was entered into the 
constructor. For query(), I have designed it so that numQueries is 
incremented at the top of the function, then we compare numQueries 
to p to see if we need to change the state. I've created a helper 
function, checkChangeState() which checks to see if numQueries is 
a multiple of p, which indicates that we need to toggle the object 
to on/off. I've created a constant int FIRSTQUERY to help the base 
and the children classes in the getSequence() function to identify 
if this is the first query and to output the unsigned version of 
the number entered into the constructor. I have a counter: 
countSuccessQ to keep track of how many times we add SEQUENCE_ADDER 
to the current number so that we can use that counter in a for loop 
in resetGen(). I've created a resetGen function that resets the 
arithmetic sequence back to the unsigned version of the initial 
number that was passed through the constructor; the state is set 
to active, numQueries is reset to 0, and countSuccessQ is set to 0.              
*/
using System;
namespace p3 
{
    public class iSeq
    {
        protected const uint SEQUENCE_ADDER = 10;
        protected const uint DEFAULT_COUNT = 0;
        protected const bool DEFAULT_ACTIVE = true;
        protected const int ERROR_HANDLING = -1;
        protected bool isActive;
        protected uint p;
        protected uint numQueries;
        protected uint currentNum;
        protected const int FIRSTQUERY = 1;
        protected uint countSuccessQ;

        public iSeq(int userStart, int pVal)
        {   
            countSuccessQ = DEFAULT_COUNT;
            numQueries = DEFAULT_COUNT;
            isActive = DEFAULT_ACTIVE;            
            currentNum = Convert.ToUInt32(userStart);
            p = Convert.ToUInt32(pVal);
        }
        public virtual int query()
        {
            numQueries++;

            checkChangeState();
            if (getState())
                return getSequence();
            else
                return ERROR_HANDLING;
        }
        /*
        checkP()
        PreConditions:  Can be active or inactive
        PostConditions: May change to active or inactive
        */
        protected virtual void checkChangeState()
        {
            if (numQueries % p == 0)
            {
                if (getState())
                    isActive = !DEFAULT_ACTIVE;
                else
                    isActive = DEFAULT_ACTIVE;
            }
        }
        protected bool getState()
        {
            return isActive;
        }
        protected virtual int getSequence()
        {   
            if (numQueries <= FIRSTQUERY)
                return (Convert.ToInt32(currentNum));
            countSuccessQ++;
            return (Convert.ToInt32(currentNum+=SEQUENCE_ADDER)); 
        }
        public virtual void resetGen()
        {
            for (int i = 0; i < countSuccessQ; i++)
                currentNum = currentNum - SEQUENCE_ADDER;
            countSuccessQ = DEFAULT_COUNT;
            isActive = DEFAULT_ACTIVE;
            numQueries = DEFAULT_COUNT;
            
        }
    }
}