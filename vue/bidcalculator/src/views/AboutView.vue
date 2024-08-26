const selected 
<template>
  <div>
    <h1>Vehicle Calculator</h1>
    <form>
      <label for="basePrice">Base Price:</label>
      <input type="number" v-model="basePrice" id="basePrice" @input="sendRequest"/>
      <br><br>
      <label for="vehiculeType">Vehicle Type:</label>
      <select v-model="vehiculeType" @change="sendRequest">
        <option disabled value="">Please select one</option>
        <option value="common">Common</option>
        <option value="luxury">Luxury</option>
      </select>
      <br><br>
      <h2>Results:</h2>
      <p>Basic Buyer Fee: {{ basicBuyerFee }}</p>
      <p>Seller Special Fee: {{ sellerSpecialFee }}</p>
      <p>Associate Fee: {{ associateFee }}</p>
      <p>Storage Fee: {{ storageFee }}</p>
      <p>Total: {{ total }}</p>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      basePrice: 0,
      vehiculeType: '',
      basicBuyerFee: 0,
      sellerSpecialFee: 0,
      associateFee: 0,
      storageFee: 0,
      total: 0
    }
  },
  methods: {
    sendRequest() {
      if(this.vehiculeType != "" && this.basePrice > 0){
        console.log("ERNESTO :", this.basePrice + " " +this.vehiculeType);
        fetch(`https://localhost:44327/api/VehicleAuction/${this.basePrice}/${this.vehiculeType}`)
        .then(response => response.json())
        .then(data => {
          this.basePrice = data.basePrice;
          this.vehiculeType = data.vehiculeType;
          this.basicBuyerFee = data.basicBuyerFee;
          this.sellerSpecialFee = data.sellerSpecialFee;
          this.associateFee = data.associateFee;
          this.storageFee = data.storageFee;
          this.total = data.total;
        })
        .catch(error => console.error(error));
      }
    }
  }
}
</script>

