# History #

**v1.2.1**

- Implemented docking windows using the dockpanelsuite library. This allows the main description window to be docked in various positions or as a floating window
- Updated the window handlers to be in-line with current coding standards
- Updated the multi-threading to use Tasks rather than delegates
- Modified the parser to use a stream rather than load entire files in one go
- Updated the Settings object to be in-line with current coding standards
- Changed solution platform target to "Any CPU", which should be more flexible
- Updated ObjectListView to v2.7.0
- Modified to allow the copying of host names using the lists context menu
- Added option to set focus to the results list after a filter item has changed. This option defaults to off and can be modified via the Options window

**v1.2.0**

- Recompiled all dependencies to be x64
- Updated project to be x64 only
- Updated ObjectListView from 2.4 to 2.6
- Moved the NetworkScanParser code into the main project
- Changed the FormsOptions listview to ObjectListView rather than standard .Net listview
- Fixed bug in the IgnorePlugins modification where the wrong item was removed
- Fixed bug when reloading items after option changes e.g. the reload used an old list of ignored plugins
- Modified the list view properties so that grid lines are removed and alternative row colours are used
- Modified the list view properties to allow the reordering of columns
- Added hour glass during import
- Fixed host summary to include open port count
- Moved all documents to markdown

**v1.1.2**

- Fixed the Nessus summary parsing. Thanks AllanS

**v1.1.1**

- Fixed some Nessus parsing bugs

**v1.1.0**

- Rewrote the entire nmap and Nessus parsing using Linq to XML, which has resulted in a far better parsing engine. Thanks MattiM for alerting me to the parsing issues with nmap v6
- Added System Type value parsing to the Nessus host summary parsing 
- Added “cvss_temporal_score” value parsing to the Nessus result parsing
- Corrected “see_also” value parsing for the Nessus result parsing
- Removed “ComponentFactory.Krypton.Toolkit” dependency by updating the About window which used the component

**v1.0.12**

- Modified to support the new Critical severity value
- Updated the host summary CSV export to include the number of Critical issues

**v1.0.11**

- Added parsing and display of various Nessus exploitability fields (exploitability_ease, exploit_available, exploit_framework_canvas, exploit_framework_metasploit, exploit_framework_core, metasploit_name, canvas_package). Thanks StevenB
- Added the ability to filter on “Exploit Available”

**v1.0.10**

- Added the ability to copy information from the Host Summary to the clipboard

**v1.0.9**

- Changed the list view settings to prevent it losing the currently selected issue, which was highly annoying!

**v1.0.8**

- Updated the CSV output to cope with \n characters better. Thanks Thomas Heinrich
- Added option to remove new lines when performing a CSV export
- Modified the delete existing database functionality since it could delete all your results if you create the DB in the same directory!

**v1.0.7**

- Added option to delete existing databases identified in the input directory. The reason for the change is that importing into a directory with an existing database could introduce duplicate records if the same data set or an over lapping data set is imported. Thanks MatthewP

**v1.0.6**

- Fixed the Severity filter combo, since the filtering didn’t work

**v1.0.5**

- Fixed the About box, since it was trying to launch the email address as a HTTP link. Thanks AidanC
- Added Host Name column to the Results list view. Not sure why I hadn’t added it, as the data was already parsed. Thanks AidanC
- Added Host Name filter combo box on the Filter tab, to allow filtering on Host Name.
- Modified the Ignore Plugins functionality to use the current users data directory rather than the application directory, which requires admin permissions to read/write. Thanks AidanC
- Added paging to the Results list view. The number of results per page can be configured via the Tools->Options window. The application defaults to 5000 results per page. 
- Added buffering to the CSV results export, so it should cope with any number of results. 
- Removed the XML export since it cannot support large volumes of data

**v1.0.4**

- Added the extra nmap fields for searching via the Search text box. So if an item has “Product: apache”, then you can search for “Product: apache” and all results with “apache” will be displayed
- Modified the text search to be case insensitive
- Fixed the menu event handlers for saving files. Thanks AllanS
- Fixed the combo box event handler (Severity). Thanks AllanS
- Fixed the Host Summary export. Thanks AllanS
- Increased width of filter combo boxes. Thanks AllanS

**v1.0.3**

- Recoded to use an ESENT database for performance and stability reasons. The previous version held everything in memory which is fast but not scalable, particular with very large result sets. An example result set with the previous version took over 17 seconds to parse, query and display 4800 results, whereas the new version takes under 3 seconds!
- Now displays the total number of results in the displayed result set in the status bar, once the load is complete
- Readded the Host Summary tab which was missing functionality from NessusViewer. Thanks AllanS
- Added the ability to export the Host Summaries as CSV

**v1.0.2**

- Added extra validation to cope with half output files
- Removed extra erroneous output in the logging window

**v1.0.1**

- Added State column for displaying nmap port state
- Added State filter drop down to allow filtering by nmap port state
- Updated to use local user application settings folder to store settings, since if it was not run as Admin then an error would occur

**v1.0.0**

- Initial Release
