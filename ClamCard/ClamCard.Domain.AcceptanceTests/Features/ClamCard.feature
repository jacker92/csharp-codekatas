Feature: User behavior scenarios

Scenario: One-Way Zone 1 Journey
	Given Michael has an Clam Card
	And Michael travels from Asterisk to Aldgate
	Then Michael will be charged $2.50 for his first journey


Scenario: One-Way Zone 1 to Zone 2 Journey
	Given Michael has an Clam Card
	And Michael travels from Asterisk to Barbican
	Then Michael will be charged $3.00 for his first journey

Scenario: Multiple journeys
Given Michael has an Clam Card
And Michael travels from Asterisk to Aldgate
And Michael travels from Asterisk to Balham
Then Michael will be charged $2.50 for his first journey
And a further $3.00 for his second journey
