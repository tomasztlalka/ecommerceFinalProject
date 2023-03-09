Feature: Coupon code

A short summary of the feature
#Background: 
#	Given I am logged in as a user


@FirstTest
Scenario: Apply discount coupon
	When I apply a discount code during checkout
	Then the total amount is correctly reduced


