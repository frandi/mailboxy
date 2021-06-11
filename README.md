# Mailboxy

Mailboxy is a simple application to watch your mailbox and chain it with certain actions.

## Installation
1. Go to the release page at https://github.com/frandi/mailboxy/releases
2. Find latest release, and download a package that matches your environment
3. Extract the package into a location in your local machine
4. Optionally set the path of the `mailboxy.exe` in your environment variable so it can be run from anywhere on your machine

## Getting Started

1. Open terminal, and go to the directory where your Mailboxy app is located
2. Run `mailboxy watch` command. If you haven't configured it before, you will be directed to complete some configs first
3. When you've completed the configuration, Mailboxy will be in the `watch` mode
4. When it detects emails coming to your mailbox that matches the defined criteria, it will let you know and pass the email objects to the defined actions in the pipeline
5. When all actions complete, Mailboxy will be in the `watch` mode again
6. If you want to quit, press `Ctrl+C` button

## Configuration
If you just want to initiate or change the default mailbox provider and actions in the pipeline, you can use the `config` command from the CLI:
1. Run `mailboxy config` command
2. Mailboxy will detect available providers. Choose one by using your `up` and `down` button and presss `Enter`
3. In the next section, Mailboxy will detect available actions. Select actions that you want to include in the pipeline by navigating with `up` and `down` button, and press `space` to select/unselect. Press `Enter` to save the config.

If you want to further change the configuration details of the providers and actions, you need to do it directly from the `config.yml` file:
1. Locate the `config.yml` file in the Mailboxy root directory. If the file doesn't exist, you can initiate it by using the `config` command as above, or you can just create the file manually
2. Open the `config.yml` file in an editor
3. Change the config details, and save it
4. You will need to restart the `mailbox watch` command so the changes will take effect

### Default Configuration
The default configuration in the `config.yml` is as follows:
```yaml
version: 1.0
defaultProvider: Gmail
activeActions:
  - PrintScreen
providers:
  - name: Gmail
    args:
      - userName:
      - password:
actions:
  - name: PrintScreen
```

## Mailboxy Providers

Mailboxy providers are located in the `/providers` folder by convention. When you're downloading the Mailboxy release package, the `Gmail` provider is included by default.

It is possible for you to create a custom library for different providers by implementing the `IMailboxyProvider` interface from the `Mailboxy.Abstract.Provider` package which you can get from NuGet.

If you put your library under the `/providers` folder, Mailboxy will automatically detect it and will be available when you run `mailboxy config` command.

When you run `mailboxy watch` without any parameters, it will use the defined `defaultProvider` from the config. If you want to watch different provider without changing the default one, you can add `--provider` parameter in the command, e.g. `mailboxy watch --provider MS365`.

## Mailboxy Actions

Mailboxy actions are located in the `/actions` folder by convention. When you're downloading the Mailboxy release package, the `PrintScreen` action is included by default. It simply displays the preview of the emails on screen.

If you want to implement custom action, you can create a library and implement the `IMailboxyAction` interface from the `Mailboxy.Abstract.Action` package which you can get from NuGet.

As long as you put your action library under the `/actions` folder, Mailboxy will be able to automatically detect it when you run `mailboxy config` command.

When you run `mailboxy watch` without any parameters, it will use the defined actions from the `activeActions` config. If you want to override this config, you can include `--actions` in the command, e.g. `mailboxy watch --actions Action1,Action2,Action3`.

When running multiple actions, please be aware that they will be run in sequence, which means the next actions will not be run untill the previous actions complete. It will affect the waiting time, and if the action is not implemented right, will affect the stability of the overall program's execution.