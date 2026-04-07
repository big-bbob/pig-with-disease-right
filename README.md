# Pig with Disease Right
Brain rot coming to a discord server near you.

# Build
Use dotnet publish to build release:

```bash
dotnet publish -c Release -o publish --no-self-contained
```

Use podman to build the container:

```bash
sudo podman build -t pig-bot -f Containerfile .
```

## Example `config.json`:

```json
{
    "token": "your token",
    "cool_servers": [
        000000000000000
        The serverIDs with your friends to
    ],
    "based_list": [
        000000000000000
        The userIDs of your cool friends
    ],
    "cringe_list": [
        000000000000000
        The userIDs of your bad friends
    ],
    "owner": You,
    "responses":[
        {
            "prompts":["something", "annoying", "they", "say"],
            "reponses": ["An epic comeback"]
        },
        {
            "prompts":["only", "needs", "one", "word"],
            "reponses": ["For the response to trigger"]
        }
    ],
    "chance_responses": [
        "Something long"
    ]
}
```