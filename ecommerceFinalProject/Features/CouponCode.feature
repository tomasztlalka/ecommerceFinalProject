Feature: Coupon code

A short summary of the feature

Background: 
	Given I am logged in as a user

@FirstTest
Scenario: Apply discount coupon
	When I apply the 'edgewords' coupon code during checkout
	Then the total amount is reduced by '15%'


