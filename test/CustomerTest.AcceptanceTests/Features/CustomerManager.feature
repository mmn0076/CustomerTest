Feature: Customer Manager
	
@create
Scenario: 01) Create A Customer
	Given site is loaded and i enter the following details
		| FirstName | LastName | DateOfBirth | PhoneNumber | Email | Address |
		| f1 | l1 | 16/4/2022 | 09172031142 | mmn@gmail.com | khonashon |
	And i prees create
	Then i should see customer created message

@create
Scenario: 02) Create A Duplicate Customer
	Given site is loaded and i enter the following Duplicate details
		| FirstName | LastName | DateOfBirth | PhoneNumber | Email | Address |
		| f2 | l2 | 16/4/2022 | 09172031142 | mmn@gmail.com | khonashon |
	And i prees create
	Then i should see Duplicate User Is Found message

@create
Scenario: 03) Create A Customer whit invalid email
	Given site is loaded and i enter the following details with wrong email
		| FirstName | LastName | DateOfBirth | PhoneNumber | Email | Address |
		| f4 | l3 | 16/5/2022 | 09172031142 | aegasfeafac | khonashon |
	And i prees create
	Then i should see Email is not Valid. message

@list
Scenario: 04) Get List of Customer
	Given site is loaded
	Then i should see customer list in tableView

@get
Scenario: 05) Get a Customer
	Given site is loaded and i press on a single customer whit name "f1"
	Then i should see customer details whit firstname "f1" in text input


@edit
Scenario: 06) Edit a Customer
	Given site is loaded and i press on a customer with name if "f1" in table to view
	And i change customer firstname to "FirstNameEdited" and press Edit btn
	Then i should see customer edited message

@delete
Scenario: 07) Delete a Customer
	Given site is loaded and i press delete customer button for customer with email "mmn@gmail.com"
	Then i should see True in result
