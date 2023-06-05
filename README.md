# MastoAltText

MastoAltText is a .Net application designed to encourage users of a specific Mastodon instance to include descriptions in their multimedia content. This application sends reminders to those who post multimedia content without descriptions, rewards those who do it to encourage them, and publishes public messages on Mastodon to recognize those who do.

This project requires a Mastodon administrator to install it, as it needs an application to have permissions on the account that sends messages to read the local messages arriving at the instance.

## Prerequisites

1. Docker installed on the machine where the application will be installed.

## Installation

1. Create a Mastodon account with administrative privileges. This account will be the one that sends messages on the instance. Mark it as "Bot" to make it clear that the account is not directly operated by a person.
2. **Create an application in your instance**: An account with administrative privileges is needed to create a new application in Mastodon. This account will be the one that sends the toots. This application must be granted enough administrative permissions to read the messages from the instance, send direct messages to other users, and send messages as "public".
3. **Modify the appsettings.Production.json file**: This file is located at the root of the project. You will need to change the name of the instance and the access token inside the "MastodonParms" node to match the created application and the URL of your instance.
4. **Customize the messages in the "AppMessages" node**: Here you can add, edit or delete messages as you prefer. The conditions format is quite intuitive and is based on the total of multimedia toots sent, and which of them have multimedia description. There is also a field to indicate how many consecutive toots have been sent with description. Below is an example of a snippet of this node with two messages:

  ```json
  "appMessages": [
    {
      "Conditions": [
        {
          "Field": "TotalToots",
          "Operator": "==",
          "Value": "1"
        },
        {
          "Field": "TootsWithDescription",
          "Operator": "==",
          "Value": "0"
        }
      ],
      "Message": "You've posted a multimedia Toot without a description. This node wants to be inclusive. Remember to put a description on your multimedia Toots so everyone can enjoy them. (This is a private message, only you can see it)",
      "MessageType": "Warn"
    },
  {
      "Conditions": [
        {
          "Field": "LastConsecutivesWithDescription",
          "Operator": "==",
          "Value": "5"
        }
      ],
      "Message": "Great! 5 consecutive Toots with description to multimedia content!! This makes this Mastodon node much more inclusive, keep it up!",
      "MessageType": "Reward",
      "PublicMessage": "{name} has sent five toots with description to multimedia content. Thanks to people like {name} this node is more inclusive! Long live {name}! 🎆🎆"
    }
  ]
  }
  ]
  }
  ```

  As you can see, the fields that can be used are:

  - "TootsWithDescription": The number of multimedia toots that this user has sent with description.
  - "TotalToots": The total number of toots with multimedia content that the user has sent.
  - "LastConsecutivesWithDescription": The number of consecutive toots that the user has sent with multimedia and description.

  With these three fields and the conditions system, we can define the messages we want to send, both to the user and publicly. The placeholder {name} will be replaced at runtime with the name of the account we are talking about.

  If you notice, the conditions within the array are evaluated as "and". There is currently no way to nest conditions or use "or" operators.

5. Start the application using docker-compose: Run the following command:

  ```bash
  docker-compose up
```

  This will start all necessary services, including the Postgres database and listening system. As a suggestion, you can add this boot command to your operating system boot system so that it starts automatically at startup, and you can manage it as a service. [Here is an example of how to achieve adding your docker-compose command in systemctl using Ubuntu](https://gist.github.com/mosquito/b23e1c1e5723a7fd9e6568e5cf91180f).

We hope you find the application useful, and please, any contribution will be more than welcome!