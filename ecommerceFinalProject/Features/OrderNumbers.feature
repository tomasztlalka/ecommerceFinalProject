Feature: Order numbers

As the checkout is completed, an order number is displayed which can then be found in order history. 

Background: 
	Given I am logged in as a user
	
@PurchasingItems
Scenario Outline: Verify order number
	Given I have added an '<item>' to cart
	When I successfully complete checkout
	Then order number shown after checkout matches the one in Orders page

	Examples:
	| item   |
	| Cap    |
	| Random |
	

