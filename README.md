
# ha-teams-watcher
This is a service that monitors [Microsoft Teams](https://www.microsoft.com/en-gb/microsoft-teams/download-app) for user presence updates (e.g. Available, Away, In a Call, etc), and pushes these updates to a [Home Assistant](https://www.home-assistant.io/) installation via a webhook.

## Why?
So that you can do cool stuff in HA based on Teams presence. For example: 

 - Turn on a warning light outside your office when you join a call.
 - Start the coffee machine at the end of a meeting.
 - Mute notifications if you are set to Do not Disturb.
 - Send a Wake on LAN packet to a device when you log off Teams for the day.

Or basically anything else you can think of.

## How?
Although it is possible to poll for Teams presence updates via the [Microsoft Graph API](https://docs.microsoft.com/en-us/graph/use-the-api), not all organisations have enabled this feature, hence this service will instead periodically check the local Teams log file for presence updates. As such, it should run locally on the same device that you are using for Teams.

**Note:** If you are using Teams for work purposes, bear in mind that depending on individual company policy, you may not be permitted to install unapproved software on your corporate device. If in doubt, check in with your local friendly IT department. I take no responsibility for users deploying this service in an unauthorized environment. :)

## Can I configure a custom name or icon for each status to send to Home Assistant?
Absolutely! Check the default status mappings in `appSettings.json` to see how this works.

## Can I listen for presence updates from another user?
No, this will only generate updates when your own status changes.

## My Home Assistant instance is locked down, how do I authenticate against it?
Currently, this service only supports two authentication schemes: 

* None (only recommended if you do not expose your HA instance to the internet.)
* Basic (you will be prompted for credentials when you start the service.)

## I run Teams on MacOS / Linux, is this supported?
I have not tested this in a non-Windows environment. However, as long as the format of the Teams log file is the same, and you update the path to the log file in `appSettings.json` to the correct value for your platform, there is no reason why this should not work.

## I have a non-English Teams installation, is this supported?
Not at the moment, however I will look into implementing user-customizable localisation of internal Teams statuses to check for.

## How do I configure Home Assistant to listen for updates from this service?
First, you will need some input text entities to hold the status and icon fields provided by the service:

```yaml
input_text:
  teams_status:
    name: Microsoft Teams status
    icon: mdi:microsoft-teams
  teams_icon:
    name: Microsoft Teams status icon
    icon: mdi:microsoft-teams
```
Second, a template sensor to pull the status text and icon into a single entity:
```yaml
template:
  - sensor:
    - name: Microsoft Teams Status
      state: "{{states('input_text.teams_status')}}"
      icon: "{{states('input_text.teams_icon')}}"    
```
Then, you will need to create an automation that listens on a webhook, and updates the input text entities with the status provided by the service:
```yaml
automation:
  - alias: Update Teams Presence
    description: ''
    trigger:
      - platform: webhook
        webhook_id: <generate a random ID>
    condition: []
    action:
      - service: input_text.set_value
        data:
          entity_id: input_text.teams_status
          value: '{{ trigger.json.name }}'
      - service: input_text.set_value
        data:
          entity_id: input_text.teams_icon
          value: '{{ trigger.json.icon }}'
    mode: single
```
Finally, you will need to update the `appSettings.json` file with the full URL to your new webhook, and an authentication scheme to use:
```json
  "HomeAssistant": {
    "WebHookUrl": "https://your-ha-instance.duckdns.org/api/webhook/your-webhook-id",
    "Authentication": {
      "Scheme": "Basic"
    }
  }
```
