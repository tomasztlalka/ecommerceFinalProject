Feature: Order numbers

A short summary of the feature

Background: 
	Given I am logged in as a user

@PurchasingItems
Scenario: Verify order number
	When I successfully complete checkout
	Then order number shown after checkout matches the one in Orders page

