The solution contains of two parts

1. a Microsoft ASP.Net MVC 2.0 web application (UI project) and 
2. a Windows Service (Scheduler project)


The Web Application
===================

It doesn't use a database but saves the results to binary files. The path to those files is set in the Web.config file in the following section:
	<appSettings>
		<add key="RepositoryPath" value="c:\Temp\" />
	</appSettings>


The Windows Service
===================


The Cake Schedule Windows Service has two tasks: 

- Send mail to the deparment responsible for cake the next cake day
- Rotate the list of deparments when a cake day has passed


INSTALLING THE SERVICE

To install the Cake Schedule Windows Service, run the Scheduler.Installer.msi install file, and install the service 
files in a suitable location.


CONFIGURING THE SERVICE

To configure the service, open the Scheduler.exe.config file found in the location specified when installing the 
service.

The settings:

TimerRunInterval - How often the timer shall run. Should be set up to run every hour. Value=3600000

HourOfDayToRunService - Hour of day when service shall kick inn and perform its work. Possible values are 0 - 23

NumberOfDaysBeforeCakeDayToSendMail - Values 0 - 13 

RepositoryPath - The location of the save files

LogPath - The service will log to this location if folders in path exists. Path shuld include file name

MailHost - The mail host the service shall use. When running in the Statkraft environment this should be set to: 
		SMTPGW.nordic.energycorp.com

FromMailAddress - The mail service requires a from email address to be able to send

MailSubject - The subject of the email

MailBody - The body of the email. Should include {0}, which will be replaced by the cake date.


STARTING THE SERVICE

To start the service, open services window, locate the CakeScheduleService, right click and select start.
