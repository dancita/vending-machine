Please complete the following coding exercise using C#.

Do not include a graphical user interface of any sort. Instead, focus on the code.

After understanding the requirements, spend around two hours writing code and tests (or a little
longer if you feel you would like to). If you run out of time and have not completed, please
mention this when submitting the code for review.

Using AI tooling to help write the code is acceptable, but please mention whether you used AI or
not when you submit your completed code.

# **Vending Machine**
In this exercise you will build the brains of a vending machine. It will accept money, make change,
maintain inventory, and dispense products. All the things that you might expect a vending
machine to accomplish.

Any money in the below refers to UK coinage in current circulation
(https://en.wikipedia.org/wiki/Coins_of_the_pound_sterling#Currently_circulating_coinage).
Pound symbols (£) and pence symbols (p) have the relationship:
100p = £1


# Features

## **Accept Coins**
*As a vendor*

*I want a vending machine that accepts coins*

*So that I can collect money from the customer*

The vending machine will accept valid coins (5p, 10p, 20p, 50p, £1.00, £2.00) and reject invalid
ones (1p, 2p).
When a valid coin is inserted the amount of the coin will be added to the current amount and the
display will be updated.
When there are no coins inserted, the machine displays INSERT COIN.
Rejected coins are placed in the coin return.

## **Select Product**

*As a vendor*

*I want customers to select products*

*So that I can give them an incentive to put money in the machine*


There are three products:

Cola for £1.00

Crisps for 50p

Chocolate for 65p

When the respective button is pressed and enough money has been inserted, the product is
dispensed and the machine displays THANK YOU.

If the display is checked again, it will display INSERT COIN and the current amount will be set to
£0.00.

If there is not enough money inserted then the machine displays PRICE and the price of the item
and subsequent checks of the display will display either INSERT COIN or the current amount as
appropriate.

## **Make Change**

*As a vendor*

*I want customers to receive correct change*

*So that they will use the vending machine again*

When a product is selected that costs less than the amount of money in the machine, then the
remaining amount is placed in the coin return.
