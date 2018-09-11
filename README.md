# Search Fight Challenge

Solution to the Search Fight challenge

## Searchfight

To determine the popularity of programming languages on the internet we want to you to write an application that queries search engines and compares how many results they return, for example:

```
    C:\> searchfight.exe .net java
    .net: Google: 4450000000 MSN Search: 12354420
    java: Google: 966000000 MSN Search: 94381485
    Google winner: .net
    MSN Search winner: java
    Total winner: .net
```

- The application should be able to receive a variable amount of words
- The application should support quotation marks to allow searching for terms with spaces (e.g. searchfight.exe “java script”)
- The application should support at least two search engines

# To add new search engines

1. Open _config.xml_
2. Use the following template:

```
  <SearchEngine Address="_______" SearchEngineName="______">
    <Parser GroupIndex="__">
      <Pattern>_______</Pattern>
    </Parser>
  </SearchEngine>
```

3. In `SearchEngineName` enter the name of the new search engine (it will be used for printing the results)
4. In `Address` enter the search addres that the your new serch engine uses (e.g. "https://www.google.com/search?hl=en&q="), make sure it contains the query identifier (in this case the "q") and the "=" character
5. In `Parser` > `GroupIndex` you can specify which value from the regex matches you will use as the result (Default value is 0 if you dont specify any).
   It's related to the Regular expression matching groups.
6. In `Pattern` enter the regex pattern that will filter the response text
