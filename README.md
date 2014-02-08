DataTableWP8
============

In progress, increasingly less crappy DataTable shim to use [SqlDbSharp](https://github.com/ruffin--/SqlDbSharp) on Windows Phone 8.

Surprised that it's possible to use DataTable in iOS and Android development (via [Xamarin](http://xamarin.com/)) but not Windows Phone, it seemed like a fun task to shim up DataTable enough to enable using the SqlDbSharp project on WP8 without changes. Next step is to integrate that project into a PCL using these files.

The code is now working against the Program.cs "test" class that's included, with `DataView.sortedTable` seemingly working and most of the `DataTable` accessors set up the way they are in `System.Data`. 

	// =========================== LICENSE ===============================
	// This Source Code Form is subject to the terms of the Mozilla Public
	// License, v. 2.0. If a copy of the MPL was not distributed with this
	// file, You can obtain one at http://mozilla.org/MPL/2.0/.
	// ======================== EO LICENSE ===============================

Shim is [MPL 2.0 licensed](http://www.mozilla.org/MPL/2.0/index.txt).  Program.cs, the "test" file, has some example code taken from the net, and is [MIT licensed](http://opensource.org/licenses/MIT).  `DictionaryBackedSet<T>` based on a reasonably highly edited (hacked? degraded?) Jon Skeet SO answer [here](http://stackoverflow.com/a/1366464/1028230).