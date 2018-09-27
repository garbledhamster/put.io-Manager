# put.io-Manager
A simple put.io manager.

I based this off of the **put.io Download manager by Gunhan Ozden** made back in 2016. 

The initial interface is similar with some major adjustments.

## Goals

- [x] Downloader
- [x] Zip Files
- [x] Transfer Managment
- [ ] Download Scheduler
- [ ] File Sharing
- [ ] File Uploader
- [ ] More Accounts Support
- [ ] Allow RSS feed reading
- [ ] Cleanup Interface
- [ ] Better Download Speed Visuals

### Authentication

To connect your put.io account to the application you must get an OAuth Token.  You can do this using the app.  Or you can make your own **Application** and use your own OAuth Token.

1. Click File > Preferences > Get Token
2. Sign into the webpage to get an authentication token.
3. Paste the Token into the textbox next to the **Get Token** button.
4. Click **Save** button

### Downloads

Once you have setup your OAuth token you can start downloading files from the provided account.

1. Expand a folder until you get to a file.
2. Right click the file > Download
3. The file will goto the default download directory.
   * The download directory is set in your preferences.
   
*Note: You can also tell the application to delete the file once downloaded.*

### Zips

You can download your directories and files as a Zip file if you want.

1. Right click a Directory or File and click **Zip & Download**.
2. Your zip will goto yoru default download directory.

### Transfers

You can start a magnet transfers to download files to put.io.

1. Find a magnet link copy it to your clipboard.
2. Select the directory in **Folders & Files** where you want your *Transfer* to go.
3. Right Click the Transfers View > New
4. Confirm the magnet and the put.io download location.
