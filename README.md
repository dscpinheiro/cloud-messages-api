# Messages REST API
This a sample project which manages messages and provides details about those messages, specifically whether or not a message is a palindrome. It supports the following operations: create, retrieve, update, and delete a single message, as well as listing all messages.

## Run API:
You can build and run the project locally using the following command (assuming that you are in the root folder of the repository)

```console
docker-compose build && docker-compose up
```

> **Note**: The [Docker Compose file](docker-compose.yml) maps port 5000 on your machine to port 80 in the container. If that port is already in use, you will see an error and have to choose a
different port.

After the application starts, you should be able to navigate to `http://localhost:5000` in your web browser and invoke the API via cURL.

```console
curl -X GET "http://localhost:5000/api/messages" --verbose --include
```

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
Passed   MessagesTests.Helpers.PalindromeTests.IsPalindrome_PalindromeWord_ReturnsTrue(word: "civic")

Total tests: 71. Passed: 71. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 4.5200 Seconds
```
--------------

## Documentation
This project uses [Swagger](https://swagger.io/) to generate the API docs, and the Swagger UI is available at the root of the web application. All operations are listed, along with their descriptions, status codes, and required parameters (if any).

![SwaggerUI](images/swagger.png)

## Project structure
![Project structure](images/project.png)

The project also leverages [CodeBuild](https://aws.amazon.com/codebuild/) and [CodePipeline](https://aws.amazon.com/codepipeline/) to guarantee that, every time a change is pushed to the master branch, the following happens:
1. [Unit tests are run](codebuild/buildspec.test.yml)
2. [New Docker image is built](codebuild/buildspec.ecr.yml)
3. Image is pushed to a registry (which in this solution is [ECR](https://aws.amazon.com/ecr/))

![CodePipeline](images/codepipeline.png)
