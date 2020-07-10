# EpisodateSharp
 [Nuget](https://www.nuget.org/packages/EpisodateSharp/) | [Direct Download](https://github.com/ArunPrakashG/EpisodateSharp/releases/download/1.0.0/EpisodateSharp.dll)
 
Unoffical .NET Standard library to wrap around `https://www.episodate.com/api` API.

## Sample Usage

```
using(EpisodateClient client = new EpisodateClient()) {
				PopularShows popularShows = await client.GetPopularShowsAsync().ConfigureAwait(false);

				if(popularShows == null) {
					Console.WriteLine("Request failed!");
					Console.WriteLine("Check your connection and restart!");
					Console.WriteLine("Press any key to exit...");
					Console.ReadKey();
				}

				for(int i = 0; i < popularShows.ShowsCollection.Length; i++) {
					TelevisionShow show = popularShows.ShowsCollection[i];
					Console.WriteLine(show.Name);
					Console.WriteLine(show.CurrentStatus);
				}

				Console.ReadKey();
			}
```

## Dependencies
* Newtonsoft.Json - for parsing api response.
* System.Collections.Immutable - for Immutable array.

## License
The MIT License (MIT)

Copyright (c) 2020 ArunPrakashG

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON INFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
