# VoiceAssistant API
## API
### Running locally
1. Go to `./api/src/VoiceAssistant.Api` (assuming you are in repo root directory).
2. Set `dotnet user-secrets` by running following command:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your postgres database secret"
```
3. Go back to repo root directory.
3. Create `.env` file (for docker environment variables).
```bash
cp .env.example .env
```
5. Edit `.env` file, to fill in your docker secrets.
6. Run the database and backend by using (assuming you are in repo root directory): 
```bash
docker compose up -d 
```

## Domain
Description of entities used in the project:
- **ShoppingItem**
> Represents an abstract item, which user is familiar with. \
> Single instance of this class is a representation of single (or multiple synonymous) phrase describing a shopping item. \
> Examples: "Bread", "Cheese", "Ham", "Milk", "Oat flakes" \
> Counter examples (not represented by this class): "Family bread 1kg", "Breakfast bread 500g", "Mlekovita Milk - 1 liter", etc.

- **Shop**
> Represents an online shop associated with a single platform or a website.

- **ShopProduct**
> Represents a concrete product with specific bar code, manufacturer, logo, name, description etc. \
> Relation-wise, represents connection between a specific **ShoppingItem** in specific **Shop**.

## Application: Business Logic
In the text below you will find description of intent processing logic. Intents are detected by mobile app and defined in [Picovoice Rhino Console](https://console.picovoice.ai/rhn/839c50e2-47b2-4d9c-b240-4ce045870544).

Endpoints available in API: 

- **shoppingItems/addToCart** - processes the following intents:
  - addToCart_single
  - addToCart_count
  - addToCart_amount
> Assumption:
> - the only `Shop` in DB is Frisco for now (Auchan, BiedronkaOnGlovo, etc. can be added later on)
> 
> If useCase == Single or Count 
> 1. Validate if `dto.ShoppingItemName` exists in Database (`ShoppingItem.Name`)
> 2. Find `ShopProduct` with Name matching to `dto.ItemName`
>    - Assume there is only one `Shop` entity (for now)
> 3. Get `ShopProduct.Url`
> 4. Delegate a job to run specific action in a browser (adds to cart) given the `shopProductUrl` and `count`
>   - if `dto.Count == null` (useCase == Single), then assume `count = 1`
> 
> If useCase == Amount
> 1. Find `ShopProduct` with Name matching to `dto.ItemName`
>   1. Fetch matching `ShopProduct`'s properties:
>      - `Description`
>      - `AmountPerPiece` - amount of product per piece (e.g. 500g, 1l, 2kg etc.)
>      - `UnitOfMeasurement` (e.g. `"g"`, `"l"`, `"kg"`)
>    - Assume there is only one `Shop` entity
> 2. Get `ShopProduct.Url`
> 3. Calculate `piecesNeeded` to add to the cart.
>   - `piecesNeeded = dto.Amount / shopProduct.AmountPerPiece` 
> 4. Delegate a job to run specific action in a browser (adds to cart) given the `shopProductUrl`

