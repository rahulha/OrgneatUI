# OrgneatUI
A simple .net based project, based on Twitter Scraping principal to download historic Tweets. This project has standard library and UI. Library can be imported to any other projects. UI is a demo and quick use tool.

# For basic instructions and Screen shots, for now please visist this page: http://www.orgneat.com/Request

# Highlight

There are many Python and Java based programs on Github. My own program is inspired by great work others have done, especially GetOldTweet program by Jefferson Henrique. This program is just an attempt to make it easy way for users to download tweets without engagging into Python learning or setting up enviornment.

The .Net code is although not Commercial grade, but pretty much close that consider lot of different scenarios such as what if Twitter do not respond, if there any timeout, if there is any issue with Tweet html etc.

One of the reason behind making GUI is ease of use. Although the library behind it in Collector folder can support consle application. The GUI makes it lot easier to provide a way to enter advanced queries without worrying about how to configure the parameters.

Based on my experience Python on my local computer takes lot of Memory and Processor. I did multi threading on Python and each thread would take at least 200 MB memory. .Net code is pretty efficient in memory handling and processor scheduling. Upon testing, even for 20 threads, program would take less than 50% Processor (Intel core i7 4th Gen) and about 500 MB in total memory. This reduces time required to download millions of tweets without compromising performance.

If you feel this code is worth taking your attention, please try it once, feel free to contribute as well.


