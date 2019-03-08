# Messages REST API
This a sample project which manages messages and provides details about those messages, specifically whether or not a message is a palindrome. It supports the following operations: create, retrieve, update, and delete a single message, as well as listing all messages.

## Run API:
You can build and run the project locally using the following command (assuming that you are in the root folder of the repository)

```console
docker-compose build && docker-compose up
```

After the application starts, you should be able to navigate to `http://localhost:5000` in your web browser.

## Run unit tests:
To execute the unit tests locally, run the following commands from the root folder.

```console
docker build --target testrunner -t cloud-aud-messages:unittests .
docker run --rm cloud-aud-messages:unittests
```
Once the container starts, you should see an output similar to this in the console.

```console
Passed   MessagesTests.Helpers.PalindromeTests.IsPalindrome_PalindromeName_ReturnsTrue(name: "Hannah")
Passed   MessagesTests.Helpers.PalindromeTests.IsPalindrome_SequenceOfRandomNumbers_ReturnsFalse
MessagesTests.Helpers.PalindromeTests.IsPalindrome_PalindromeWord_ReturnsTrue(word: "civic")

Total tests: 71. Passed: 71. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 4.5200 Seconds
```