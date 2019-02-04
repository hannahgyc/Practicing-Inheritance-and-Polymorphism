/*
Hannah Chang
CPSC 3200
Created: 01/27/19 - EDITED: 01/29/19, 01/30/19, 01/31/19, 02/01/19
p3.cs - C#

DRIVER OVERVIEW:    

I create an array of iSeq objects of constant size, SIZE. I call 
initialize(iSeq) which initializes every index to a iSeq object 
by isolating the initialization process and repeats 
until the array is full. I initialize these objects with 2 random 
number generator that generates 1 random number between constants 
RND_MIN and RND_MAX and the 2nd random number between const ints:
MIN_P and MAX_P; where the first passed in number represents the
first number in the sequence and the second number represents p. 
I then call testObjects(iSeq) and go through the entire array 
TEST_TIME number of times by either querying (TEST_QUERIES number 
of times) or resetting. We use a switch statement and a rng to 
switch between reset and query (we use case 0 to test query and we
do this by qeurying TEST_QUERIES number of times, we use case 1 to 
test the reset function. I designed it like this so that we can 
easily see that the program is functioning correctly).
                    
PROGRAM OVERVIEW:   
When we initially construct an iSeq obj, the currentNum is passed 
in and converted into an unsigned integer and a random number 
between MIN_P and MAX_P-1 is passed in as p. The state of the 
object is intialized to active. When we query the iSeq object, we 
increment the numQueries. Then, we check to see if this is the pth 
request and if it is, then we change the state of the object. If 
the new state is active, we return the next number in the sequence 
(if this is the first query then we return the first number in the 
sequence). Else, if the new state is inactive, we output an 
ERROR_HANDLING. The client can reset to the original value that was 
entered into the program (unsigned version) at any point in the 
program.

When we initially construct a randomS object, a child of iSeq, we 
initialize it through the base classes constructor. When we query 
the randomS object, we use the base classes query() function. 
After numQueries is incremented, we call the overrided function: 
checkChangeState() to randomly change the state of the object to on 
or off. If the new state is active, we check the next number in the 
sequence (if this is the first query then we return the first number 
in the sequence) to see if this next number is a multiple of p. If 
it is a multiple of p, we don't save this number and then we output 
an ERROR_HANDLING. If it is not a multiple of p, we save and output 
this number. Otherwise, if the new state is inactive, then we ouput 
an ERROR_HANDLING. The client can reset to the original value that 
was entered into the program (unsigned version) at any point in the
program.

When we initally construct an oscillateS object, a child of iSeq, we 
initialize it through the base classes constructor. When we query the 
oscillateS object, we use the overrided query() function. We increment 
numQueries and then we call the overrided function checkChangeState() 
which increments countK and checks to see if this is the pth request 
and if it is, then we change the state of the object. If the new state 
is inactive, we call the base classes function: getSequence() to get 
the next number in the sequence (if this is the first query then we 
return the first number in the sequence). Otherwise, if the new state is 
active then we reset the values by calling resetKValues() and then flip 
the signs to neg/pos which alternates everytime we have an active state. 
Everytime we reset the arithmetic whether it's through the client or 
because it is one of the first k changes, the number of queries is reset 
back to 0 but is incremented before outputting the first number in the 
sequence. Note that if a reset happens due to the fact that it is one of 
the first k changes, after outputting the ressetted number, the program 
counts that output as the first query (first of p queries). The client 
can reset to the original value that was entered into the program 
(unsigned version) at any point in the program.

ASSUMPTIONS:        
We assume that the size of the array holding iSeq, randomS, and 
oscillateS is constant and is 15. We also assume that the only integers 
used to initialize each object are integers from 0 to 101 (we note that
all ints passed into the constructor will be converted into uints). We 
also assume that we query a constant total of 50 times for each object 
in the array, I believe that it repeats enough to show that the program 
is doing all of what it is supposed to do. We assume that p will be a 
value from MIN_P (1) to MAX_P-1 (11-1). we assume that k is constant.
*/
using System;
namespace p3
{
    class Driver
    {
        const int SIZE = 30;
        const int MIN_START = 0;
        const int MAX_START = 101;
        const int TEST_TIMES = 25;
        const int TEST_QUERIES = 10;
        const int MIN_P = 1;
        const int MAX_P = 6;
        const int CASE_ZERO = 0;
        const int CASE_ONE = 1;
        const int MAX_CASE = 2;
        
        static void Main()
        {
            Console.WriteLine("Welcome!");
            Console.WriteLine();
            
            iSeq [] arr = new iSeq[SIZE];
            for (int i = 0; i < SIZE; i++)
                arr[i] = GetObj(arr, i);
            
            for (int i = 0; i < SIZE; i++)
            {   
                Console.WriteLine("I've created a new game for you!");
                testObjects(arr, i);
            }
        }
        public static iSeq GetObj(iSeq [] arr, int index)
        {
            if (index%3 == 0) 
            {   Console.WriteLine("== 0");
                return (new iSeq(rng(MIN_START,MAX_START), rng(MIN_P,MAX_P)));
            }
            else if (index%3 ==1) 
            {
                Console.WriteLine("== 1");
                return (new randomS(rng(MIN_START,MAX_START), rng(MIN_P,MAX_P)));
            }
            else if (index%3 ==2) 
            {
                Console.WriteLine("== 2");
                return (new oscillateS(rng(MIN_START,MAX_START), rng(MIN_P,MAX_P)));
            }
            else return null;
        }
        public static void testObjects(iSeq [] arr, int index)
        {
            for (int j = 0; j < TEST_TIMES; j++)
                {   
                    int caseNum = rng(CASE_ZERO, MAX_CASE);
                    switch (caseNum)
                    {
                        case CASE_ZERO:
                            for (int x = 0; x < TEST_QUERIES; x++)
                                Console.WriteLine(arr[index].query());
                            break;
                        case CASE_ONE:
                            Console.WriteLine("resetting...");
                            arr[index].resetGen();
                            break;
                    }   
                }
        }
        static int rng(int min, int max)
        {
            Random random = new Random(); 
            return random.Next(min, max);
        }
    }
}
