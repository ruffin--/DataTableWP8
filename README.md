DataTableWP8
============

In progress crappy DataTable shim to use [SqlDbSharp](https://github.com/ruffin--/SqlDbSharp) on Windows Phone 8.

Surprised that it's possible to use DataTable in iOS and Android development (via [Xamarin](http://xamarin.com/)) but not Windows Phone, it seemed like a fun task to shim up DataTable enough to enable using the SqlDbSharp project without changes.

**Right now, after a couple hours' play, the shim's not even logically sound**, but it's pretty clear what direction this is taking.  Creating usable DataTables is about a QuickSort and a couple of `DictionaryBackedSet<T>` extensions away from happening.

	// =========================== LICENSE ===============================
	// This Source Code Form is subject to the terms of the Mozilla Public
	// License, v. 2.0. If a copy of the MPL was not distributed with this
	// file, You can obtain one at http://mozilla.org/MPL/2.0/.
	// ======================== EO LICENSE ===============================

Shim is [MPL 2.0 licensed](http://www.mozilla.org/MPL/2.0/index.txt).  Test file has some example code taken from the net, and is [MIT licensed](http://opensource.org/licenses/MIT).  `DictionaryBackedSet<T>` based on a reasonably highly edited (hacked? degraded?) Jon Skeet SO answer [here](http://stackoverflow.com/a/1366464/1028230).
