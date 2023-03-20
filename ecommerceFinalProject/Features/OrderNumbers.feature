Feature: Order numbers

As the checkout is completed, an order number is displayed which can then be found in order history. 

Background: 
	Given I am logged in as a user
	And I have added an 'item' to cart

@PurchasingItems
Scenario: Verify order number
	When I successfully complete checkout
	Then order number shown after checkout matches the one in Orders page

