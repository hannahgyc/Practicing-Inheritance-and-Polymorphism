# Practicing-Inheritance-and-Polymorphism
Design a class hierarchy of iSeqs, where each iSeq object, upon a query, returns the next number 
in a sequence; the generation of such values may be reset. 

1)   If ‘on’, an iSeq object returns 
the next number in an arithmetic sequence; its on/off state is toggled with every pth request, 
where p is an internal, stable value that varies from object to object; a change in on/off state 
does NOT reset the sequence.
2)   A  randomS  acts  like  a  iSeq,  except  that  it  returns  the  
next  number  in  a  randomsequence, unless that number is a multiple of p, and arbitrarily 
toggles on/off state.
3)   If ‘off’, an oscillateS acts exactly like an iSeq; otherwise, it alternates the sign of 
the output number and resets the arithmetic sequence for the first k changes of on/off.   
