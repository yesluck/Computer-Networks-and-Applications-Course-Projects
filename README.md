# Computer-Networks-and-Applications-Course-Projects

Course Projects in Course "Computer Networks and Applications" in Tsinghua University

## Program File Descriptions
The following program files are arranged in alphabetical order:
Chat.cs		Chat with a friend
Friend.cs	Friend card label
Login.cs	Login
Main.cs		Main interface
MultiChat.cs	Group chatting (Chat with multiple friends)
NewFriend.cs	Add new friends
Portrait.cs	Change the avatar

## Executable program
MicroChat.exe file in src folder, listening port numbers: 8010, 8030.

## Programming language and IDE
This program was written in C# language with Visual Studio 2013 Community.

## Program function and the way to use it
### Change the avatar
Move the mouse to the avatar part of the login interface, the mouse shape will become a finger. After clicking, you will enter the interface of “Select the Avatar (选择头像)”. Click on any desired image, the avatar selection is successful. the "Select the Avatar (选择头像)" interface will be closed, and the avatar of your login screen will change.

### Input user name and password
The user name and password in the program has been set to my student number (2014011541) and the given password (net2016).

### Login
Click the "→" button on the right side of the password box, and the program will initiate a request to the server. If the user name and password is correct and the network environment is fine, you will be successfully logged in.

### Add friends
Click the “Add Friends (添加好友)” button on the right side of the main interface, “Add Friends (添加好友)” interface will appear. Enter a student number to add a friend, click on “Inquiry and Add (查询并添加)”, then a request will be sent to the student. If he/she agrees, his/her account will be added to your friend list. Click on the “Refresh Friend (刷新好友)” button to confirm that.

### 