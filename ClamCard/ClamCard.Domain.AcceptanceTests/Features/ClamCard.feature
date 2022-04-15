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

Scenario: Multiple Journeys including Zone B reaching daily cap
	Given Michael has an Clam Card
	And Michael travels from Asterisk to Barbican
	And Michael travels from Barbican to Balham
	And Michael travels from Balham to Bison
	And Michael travels from Bison to Asterisk
	Then Michael will be charged $3.00 for his first journey
	And a further $3.00 for his second journey
	And a further $2 for his third journey
	And a further $0.00 for his fourth journey

Scenario: Multiple Journeys Zone A reaching daily cap
	Given Michael has an Clam Card
	And Michael travels from Asterisk to Aldgate
	And Michael travels from Aldgate to Angel
	And Michael travels from Angel to Antelope
	And Michael travels from Antelope to Asterisk
	Then Michael will be charged $2.50 for his first journey
	And a further $2.50 for his second journey
	And a further $2 for his third journey
	And a further $0.00 for his fourth journey

Scenario: Multiple Journeys Zone A on two different days
	Given Michael has an Clam Card
	And Michael travels from Asterisk to Aldgate
	And Michael travels from Aldgate to Angel
	And Michael travels from Angel to Antelope
	And Michael sleeps for 1 day
	And Michael travels from Antelope to Asterisk
	Then Michael will be charged $2.50 for his first journey
	And a further $2.50 for his second journey
	And a further $2 for his third journey
	And a further $2.5 for his fourth journey

Scenario: Multiple Journeys including Zone B on two different days
	Given Michael has an Clam Card
	And Michael travels from Asterisk to Barbican
	And Michael travels from Barbican to Balham
	And Michael travels from Balham to Bison
	And Michael sleeps for 1 day
	And Michael travels from Bison to Asterisk
	Then Michael will be charged $3 for his first journey
	And a further $3 for his second journey
	And a further $2 for his third journey
	And a further $3 for his fourth journey

Scenario: Multiple Journeys Zone A on different days reaching weekly cap
	Given Michael has an Clam Card
	And Michael travels reaching daily cap on zone A
	And Michael sleeps for 1 day
	And Michael travels reaching daily cap on zone A
	And Michael sleeps for 1 day
	And Michael travels reaching daily cap on zone A
	And Michael sleeps for 1 day
	And Michael travels reaching daily cap on zone A
	And Michael sleeps for 1 day
	And Michael travels reaching daily cap on zone A
	And Michael sleeps for 1 day
	And Michael travels reaching daily cap on zone A
	And Michael sleeps for 1 day
	And Michael travels reaching daily cap on zone A
	Then Michael will be charged $40 in total

Scenario: Multiple Journeys Zone A on different days reaching monthly cap
	Given Michael has an Clam Card
	And Michael travels for a week reaching weekly cap on zone A
	And Michael sleeps for 1 day
	And Michael travels for a week reaching weekly cap on zone A
	And Michael sleeps for 1 day
	And Michael travels for a week reaching weekly cap on zone A
	And Michael sleeps for 1 day
	And Michael travels for a week reaching weekly cap on zone A

	Then Michael will be charged $145 in total
