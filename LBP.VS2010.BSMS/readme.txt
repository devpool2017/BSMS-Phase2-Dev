1. copy devfault website.
2. open solution in vs
3. rename files accordingly
	3.1. rename solution
	3.2. rename website.
	3.3. right click website > properties
		3.3.1 change assembly name and root namespace
4. change the namespace in existing files (with inherits keywqord in aspx)
	4.1 rename master files 
		4.1.1 login.master
		4.1.2 site.master
	4.2 rename default pages
		4.2.1 views/error pages
			4.2.1.1 expiredsession.aspx
		4.2.2 views/login
			4.2.2.1 login.aspx
			4.2.2.2 logout.aspx
			4.2.2.3 welcome.aspx
		4.2.3 views/test
			4.2.3.1 erictrydetail.aspx
5. build. close vs 2010
6. go to file explorer - rename the website folder.
7. open the .sln file in notepad. change the website vbproj directory
8. open solution in vs2010. set the website as startup project
9. change the systemname and systemcode in appsettings
10. build. code away!


	

		