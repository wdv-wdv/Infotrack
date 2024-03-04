<template>
  <div>
    <h1>Search Ranking</h1>
    <div v-if="error" style="color:crimson">
      <br/>
      <h1>Error:</h1>
      <h2>{{ error }}</h2>
    </div>

    <template v-if="!error">
      <form @submit.prevent="search">
        <div class="input-group">
          <label for="searchInput">Search:</label>
          <input type="text" id="searchInput" v-model.trim="searchQuery" placeholder="Enter search term">
        </div>

        <div class="input-group">
          <label for="urlInput">URL:</label>
          <input type="text" id="urlInput" v-model.trim="urlQuery" placeholder="Enter url">
        </div>

        <button :disabled="!canSearch" type="submit">{{ isLoading ? 'Searching - please wait' : 'Search' }}</button>
      </form>

      <div v-if="ranking > 0">
        <h2>Search Results:</h2>
        Url was listed {{ranking}}{{ getOrdinal(ranking) }} in the search engine search result
      </div>

      <div v-if="ranking == 0">
        <p>No results found for URL</p>
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { defineComponent} from 'vue';

export default defineComponent({
  name: 'SearchForm',
  data() {
    return {
      searchQuery: 'land registry search',
      urlQuery: 'www.infotrack.co.uk',
      isLoading: false,
      error: '' as string,
      ranking: -1
    };
  },
  methods: {
    async search(): Promise<void> {
      const apiUrl = '/api/PerformSearch';
      const headers = {
        Accept: "application/json",
        "Content-Type": "application/json",
      };
      this.isLoading = true;
      this.ranking = -1
      fetch(apiUrl, {
        method: "POST",
        headers,
        body: JSON.stringify({
          "searchTerm": this.searchQuery,
          "url": this.urlQuery
        })
      })
        .then(response => response.text())
        .then(data => {
          this.ranking =  Number(data);
          this.isLoading = false;
        })
        .catch(err => {
          console.log(err);
          this.error = "Server Down. Please try again later"
          this.isLoading = false;
        });
    },
    async checkServer(): Promise<void> {
      const apiUrl = '/api/PerformSearch';
      fetch(apiUrl)
        .then(response => response.text())
        .then(data => {
          this.error = ((data == "Its all good") ? '' : 'Server Down. Please try again later');
        })
        .catch(err => {
          console.log(err);
          this.error = "Server Down. Please try again later"
        });
    },
    getOrdinal(n : number) {
      let ord = 'th';

      if (n % 10 == 1 && n % 100 != 11)
      {
        ord = 'st';
      }
      else if (n % 10 == 2 && n % 100 != 12)
      {
        ord = 'nd';
      }
      else if (n % 10 == 3 && n % 100 != 13)
      {
        ord = 'rd';
      }

      return ord;
    }
  },
  computed: {
    canSearch()
    {
      return (!this.isLoading &&
        this.searchQuery.length > 0 &&
        this.urlQuery.length > 0
      )
    }
  },
  mounted() {
    this.checkServer()
  }
});
</script>

<style scoped>
.input-group {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
}

.input-group label {
  min-width: 80px;
  text-align: right;
  margin-right: 10px;
}
</style>
