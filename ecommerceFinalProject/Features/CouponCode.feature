Feature: Coupon code

The user can apply a discount/coupon code before proceeding to checkout which reduces the price by a certain percentage

Background: 
	Given I am logged in as a user

@PurchasingItems
Scenario: Apply discount coupon
	When I apply the 'edgewords' coupon code during checkout
	Then the total amount is reduced by '15%'


