/*
Hannah Chang
CPSC 3200
Created: 01/27/19 - EDITED: 01/29/19, 01/30/19, 01/31/19, 02/01/19
oscillateS.cs - C#

CLASS INVARIANTS:           
Whether the object is on/off, it has the same p value. K is a 
constant integer that is the same for all oscillateS objects.
Program only accepts integers but all negative numbers inputed 
to be used as the initial number in the sequence will be changed 
to positive (uints). -1 is an error handling number. Everytime
we reset the arithmetic whether it's through the client or 
because it is one of the first k changes, the number of queries 
is reset back to 0 but is incremented after outputting the first 
number in the sequence. The client can reset to the original value 
that was entered into the program (unsigned version) at any point 
in the program. Note that if a reset happens due to the fact that 
it is one of the first k changes, after outputting the ressetted 
number, the program counts that output as the first query (first 
of p queries).
                            
INTERFACE INVARIANTS:       
When outputing the next number in the sequence, we are taking the 
absolute value of each previous integer (even if the previous 
integer was a negative). Reseting the sequence means that we 
will delete the last k vals (hidden num in the program), may 
delete up to the original input. When switching the signs
of the numbers, it is not always linear (back and forth).
                            
IMPLEMENTATION INVARIANTS:  
K is a constant integer that is the same for all oscillateS 
objects. The first k number of changes of state is how many 
times we reset the arithmetic sequence. We override the query 
function by incrementing count and checking the state to see 
if count is a multiple of p and THEN if the oscillateS object 
is inactive, it acts as a iSeq object by returning the next 
number in the sequence and if the oscillateS is active: we call 
a function changeSign() and we reset the the entire arithmetic 
sequence back to the original number (if countK <= k). Whether 
oscillateS is active or inactive, the only way to change the 
state is to have count request be a multiple of p. I've created 
an uint variable 'countK' in order to keep track of the number 
of times isActive is toggled from on to off and we can compare 
'countK' to 'k' and if countK <= k then we reset the arithmetic 
sequence. I override checkChangeState() which checks to see if 
numQueries is a multiple of p, which indicates that we need to 
toggle the object to on/off. I've created a numQueries (in iSeq) 
that counts the number of times we need to subtract the 
SEQUENCE_ADDER from the currentNum to get the resetted number. 
I've created a changeSign(int) which uses a uint countSign 
(initialized to DEFAULT_COUNT (0)) to keep track of signs changing. 
The function alternates the sign of the next number in the sequence 
from neg to pos and pos to neg whenever the function is called 
(eg first time calling changeSign return neg sign, second time calling 
changeSign return pos sign, etc... note that function calls may 
not be linear). The negative number is not saved, but rather just 
used to display to the client. In query, numQueries is incremented 
and checkChangState is called prior to checking the state. I create 
a constant int MULTIPLIER to help flip the signs of the currentNum 
in the changeSign() function and to practice the opposite of 
hardcoding. I have created a function resetKValues() that uses
numQueries to help count how many times we need to subtract 
SEQUENCE_ADDER from currentNum. We set i = 1 in the for loop 
because we don't want to subtract the SEQUENCE_ADDER from the
original int that was sent into the constructor; we use the 
argument i<numQueries-1 because numQueries is incremented prior to 
adding SEQUENCE_ADDER to the currentNum. We also reinitialize 
numQueries to 0 and then increment it because we realize that it 
is currently in the resetKValues() function that was called by 
checkChangesState() that was called by query(). We override the 
resetGen() function because we use a different counter method 
and have it so that we loop until we end up with the original 
number that was sent into the constructor (unsigned version); 
we then reset all the countK, countSign, and numQueries and set
the state to active. 
*/
namespace p3
{
    public class oscillateS : iSeq
    {
        private const uint k = 3;
        private const int RND_MIN = 1;
        private const int RND_MAX = 6;
        private const int MULTIPLIER = 2;
        private uint countK;
        private uint countSign;
        public oscillateS(int userStart, int p) : base(userStart, p)
        {
            countK = DEFAULT_COUNT;
            countSign = DEFAULT_COUNT;
            
        }
        public override int query()
        {   
            numQueries++;
            checkChangeState();
            
            if (!getState())
                return getSequence();
            
            else
            {   
                int nextNum = changeSign(getSequence());
                return nextNum;
            }
        }
        private void resetKValues()
        {
            for (int i = 1; i < numQueries-1; i++)
                currentNum = currentNum - SEQUENCE_ADDER;
            numQueries = DEFAULT_COUNT;
            numQueries++;
            
        }
        public override void resetGen()
        {
            for (int i = 1; i < numQueries; i++)
                currentNum = currentNum - SEQUENCE_ADDER;
            isActive = DEFAULT_ACTIVE;
            numQueries = DEFAULT_COUNT;
            countSign = DEFAULT_COUNT;
            countK = DEFAULT_COUNT;
        }
        /*
        checkP()
        PreConditions:  Can be active or inactive
        PostConditions: May change to active or inactive
        */
        protected override void checkChangeState()
        {
            if (numQueries % p == 0)
            {
                if (getState())
                    isActive = !DEFAULT_ACTIVE;
                else
                {
                    isActive = DEFAULT_ACTIVE;
                    countK++;
                    if (countK <= k)
                        resetKValues();
                }
            }
        }
        private int changeSign(int nextNum)
        {   
            if (countSign == DEFAULT_COUNT)
            {
                countSign++;
                return (nextNum-(MULTIPLIER*nextNum));
            }
            countSign--;
            return nextNum; 
        }
    }
}