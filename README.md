NForum
======

NForum is an open source Bulletin Board System / Discussion Forum engine for .NET. The source code is 100% done in C#, using .NET version 4.5.1.

NForum is a no BS forum engine implementation, including the basic features needed to run a modern day forum, and an easy way of adding your own needed features, without changing the code base.

## Status

We're still a very young project, and are not ready for an alpha release just yet.

## Project Features

* Written in C#, on .NET v4.5.1
* Using services, repositories, providers and other well known patterns, to make it easy to replace parts if needed.
* A solution that is not tied to any web server technology (it's just an engine)
* Events (pub/sub) to make it easy to plug new features into the engine
* Entity Framework implementation included for storing elements in a SQL database
* Lucene.NET implementation included for searching
* MIT License

## Forum Features

* Unlimited number of categories
* Unlimited number of forums and sub-forums
* Follow/unfollow forums and topics to get notified when a new topic/message is posted
* Forum/Topic tracker to keep track of unread/read forums and topics
* Topic types are: Regular, sticky and announcement
* Topic state includes: Deleted, moved, quarantined and locked
* Post state includes: Deleted and quarantined

## Roadmap

* Run on ASP.NET Core 1.0
* Test the forum with an alternate (document?) data store
* Support cloud/web farm/multi instance hosting
* Optimized for cloud hosting, providers for (Redis?) cache, blob storage, queues for async work, etc.

## AppVeyor Build Status

[![Build status](https://ci.appveyor.com/api/projects/status/r8t8tqqidk7bnf3q/branch/master?svg=true)](https://ci.appveyor.com/project/steentottrup97321/nforum/branch/master)
