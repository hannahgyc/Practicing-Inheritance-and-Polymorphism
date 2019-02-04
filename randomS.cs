/*
Hannah Chang
CPSC 3200
Created: 01/27/19 - EDITED: 01/29/19, 01/30/19, 01/31/19, 02/01/19
randomS.cs - C#

CLASS INVARIANTS:          
The object may toggle on/off per every query(). If the next num
in the sequence is a multiple of p, then the program returns an
error handling (-1) which is not a valid value in the sequence.
Sequence is a positive number sequence (including 0). Program 
only accepts integers but all negative numbers inputed to be used 
as the initial number in the sequence will be changed to positive. 
-1 is an error handling number. The client can reset to the original 
value that was entered into the program (unsigned version) at any 
point in the program.

INTERFACE INVARIANTS:       
The sequence is random and one is unable to guess the sequence 
pattern (there is no pattern). It may be possible that all outputs 
will be error handling (-1) when p is set to 1 because everything 
is a multple of 1. Zero is a multiple of itself, and no other 
numbers.

IMPLEMENTATION INVARIANTS:  
I override getSequence() so that the function returns an error 
handling (-1) if the next number in the random sequence is a 
multiple of 'p'. This function also creates a random sequence 
by starting with currentNum (initialized to the unsigned version 
of the starting num that was passed into the constructor) and 
adds a random number to itself (only if it is not divisible by 
p, does not add to itself if it is divisible by p). The random 
number that is added to currentNum in the overrided getSequence()
function is a number between constant RND_MIN (1) and constant 
RND_MAX (101). I override checkChangeState() so that the 
function changes the state of the object to on and off depending 
on the outcome of a rng(CASE_MIN, CASE_MAX); where CASE_MIN is a 
const int set to 0 and CASE_MIN is a const int set to 4. If the
random num is an even num then we set the object to active,
otherwise we set the object to inactive. P value is derived 
from the parent class, iSeq. Zero is not a multiple of any
number, so when zero is the first number in the sequence, 
we will return that number upon the first query(). I've 
created a rng() function to aid the arbitrary case switching 
of the states and for the random number addition to the 
sequence. I've also created a uint: firstNum that holds the 
first number in the sequence; I do this because we need 
this number to be able to reset the function to the first 
value that was entered into the program (unsigned version). 
I have a counter: countSuccessQ to keep track of how many 
times we add SEQUENCE_ADDER to the current number so that 
we can keep track of when to output the original num in 
getSequence().
*/
using System;
namespace p3
{
    public class randomS : iSeq
    {
        private const int RND_MIN = 1;
        private const int RND_MAX = 101;
        private const int CASE_MIN = 0;
        private const int CASE_MAX = 4;
        private uint firstNum;

        public randomS(int userStart, int p) : base(userStart, p)
        {
            firstNum = Convert.ToUInt32(userStart);
        }
        /*
        checkP
        PreConditions:  Can be active or inactive
        PostConditions: May change to active or inactive.
        */
        protected override void checkChangeState()
        {
            uint caseNum = Convert.ToUInt32(rng(CASE_MIN, CASE_MAX));

            if (caseNum % 2 == 0)
                isActive = DEFAULT_ACTIVE;
            else
                isActive = !DEFAULT_ACTIVE;
        }
        protected override int getSequence()
        {   
            if (countSuccessQ == DEFAULT_COUNT)
            {
                countSuccessQ++;
                return (Convert.ToInt32(currentNum));
            }
            uint nextNum = (Convert.ToUInt32(currentNum + rng(RND_MIN, RND_MAX)));

            if (nextNum % p == 0)
                return ERROR_HANDLING;
            
            countSuccessQ++;
            currentNum = nextNum;
            return (Convert.ToInt32(currentNum));
        }
        public override void resetGen()
        {
            currentNum = firstNum;
            countSuccessQ = DEFAULT_COUNT;
            isActive = DEFAULT_ACTIVE;
            numQueries = DEFAULT_COUNT;
        }
        protected static int rng(int min, int max)
        {
            Random rndm = new Random(); 
            return rndm.Next(min, max); 
        }
    }
}