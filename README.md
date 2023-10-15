# MYP Senior Engineer Test

Build an investment app API. 

It should allow you to:
* Add an investment
    
    * An investment should include:
        * Name
        * Start Date
        * Interest Type: Simple or Compound
        * Interest Rate
        * Principle Amount
    * The name of an investment should be unique
* Update an investment
* Delete an investment
* Calculate Investment
    * Returning:
        * Name
        * Start Date
        * Interest Type: Simple or Compound
        * Interest Rate
        * Principle Amount
        * Current value of the investment rounded to the nearest month
    
    
    ```
    Acceptance Criteria 1
    GIVEN an investment is of type 'Simple'
    WHEN interest is calculated
    THEN a value is returned equal to  A = P(1 + rt)
    AND the period is rounded to the nearest month

    Where:
    A = Total Accrued Amount (principal + interest)
    P = Principal Amount
    I = Interest Amount
    r = Rate of Interest per year in decimal
    t = Time Period involved in months or years
    See https://www.calculatorsoup.com/calculators/financial/simple-interest-plus-principal-calculator.php

    Acceptance Criteria 2
    GIVEN an investment is of type 'Compound'
    WHEN interest is calculated
    THEN a value is returned equal to  A = P(1 + r/n)nt
    AND the compounding perdiod is Monthly
    AND the period is rounded to the nearest month (i.e. If Start Date is 10/01/2021 and today is 28/03/2023 then rounded months is 27, if start date is 02/02/2023 and today is 27/02/2023 then rounded months is 1)
    
    Where:
    A = Accrued amount (principal + interest)
    P = Principal amount
    r = Annual nominal interest rate as a decimal
    n = number of compounding periods per unit of time
    t = time in decimal years; e.g., 6 months is calculated as 0.5 years. 
    See https://www.calculatorsoup.com/calculators/financial/compound-interest-calculator.php

    Acceptance Criteria 3
    GIVEN an investment was added 2 years ago
	AND Start Date is 29/01/2021
	AND the interest type is Simple
	AND the interest rate is 3.75%
	AND the principle amount is 1000
    WHEN the investement calculate investment endpiont is called on the 1/04/2023
    THEN the investment is returned
    AND the current value of the investment returned is 1081.25

    Acceptance Criteria 4
    GIVEN an investment was added 2 years ago
	AND Start Date is 29/01/2021
	AND the interest type is Compound
	AND the interest rate is 3.75%
	AND the principle amount is 1000
    WHEN the investement calculate investment endpiont is called on the 1/04/2023
    THEN the investment is returned
    AND the current value of the investment returned is 1,084.50

    ```
* Authentication is not needed
* In memory DB is acceptable
* It should be extensible
* Quality should be high



Instructions:
* Add the code as is to a new git repo
   ```bash  
   git init -b main
   git add .
   git commit -m "Initial Commit"
   ``` 

* Please use your commits to tell a story (i.e. not one big commit) 
* Be ready to answer questions about your choices
* When you are complete bundle up your repo. This will create a file called myptest.bundle 
    ```bash 
    git bundle create myptest.bundle main
    ``````
* Send an email with:
    * myptest.bundle
    * Summary of what you have improved including why 
    * Any changes/improvements de-prioritised and not implemented with notes on your decision   








