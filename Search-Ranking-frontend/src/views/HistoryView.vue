<template>
  <div class="history">
    
    <h1>Ranking History</h1>

    <div v-if="error" style="color:crimson">
      <br/>
      <h1>Error:</h1>
      <h2>{{ error }}</h2>
    </div>

    <template v-if="!error">
      <template v-if="isLoading">
        <br/>
        Loading search ranking history ...
      </template>

      <template v-if="!isLoading">
        <div v-if="searchResults.length > 0">
          <div>
            <Bar :data="data" :options="options" style="height: 300px; position: 'relative'" />
          </div>
          <h2>Search Results:</h2>

          <table style="width: 100%;">
            <tr style="font-weight: bold; text-align: left; text-decoration: underline;">
              <th>Date</th><th>Ranking</th><th>Search Term</th><th>Search Engine</th>
            </tr>
            <tr v-for="(result, index) in searchResults" :key="index">
              <td>{{ formatDate(result.time) }}</td>
              <td>{{ result.ranking }}</td>
              <td>{{ result.searchTerm }}</td>
              <td>{{ result.searchEngine }}</td>
            </tr>
          </table>
        </div>

        <div v-if="searchResults.length === 0">
          <p>No results found.</p>
        </div>
      </template>
    </template>
  </div>
</template>

<script lang="ts">
import { defineComponent} from 'vue';

import { Bar } from 'vue-chartjs'
import { Chart as ChartJS, Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale } from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

interface SearchResult {
  searchTerm: string;
  searchEngine: string;
  ranking: number;
  time: Date;
}

interface Dataset {
    label: string;
    backgroundColor: string;
    data: number[];
}

export default defineComponent({
  name: 'HistoryView',
  components: {
    Bar
  },
  data() {
    return {
      isLoading: false,
      error: '' as string,
      searchResults: [] as SearchResult[],
      data: {
        labels: [] as string,
        datasets: [] as Dataset
      },
      options: {
        maintainAspectRatio: false
      }
    };
  },
  methods: {
    async getHistory(): Promise<void> {
      const apiUrl = '/api/History';
      this.isLoading = true;
      fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
          //console.log(data);
          this.searchResults = data;
          this.isLoading = false;
        })
        .catch(err => {
          console.log(err);
          this.error = "Server Down. Please try again later"
          this.isLoading = false;
        });
    },
    formatDate(value: Date) {
      return new Date(value).toLocaleString();//.toLocaleDateString();
    },
  },
  mounted() {
    this.getHistory()
  },
  watch: {
    searchResults: {
      handler: function (val,) {
        console.log('buildChart');
        //prepare labels: dates
        let min = new Date(new Date(val.reduce(function (a, b) { return a < b.time ? a : b.time; },new Date(2000,1,1))).toDateString());
        let max = new Date(new Date(val.reduce(function (a, b) { return a > b.time ? a : b.time; },new Date(2100,1,1))).toDateString());
        let dates = [] as Date
        while (min <= max) {
            dates.push(new Date(min));
            min.setDate(min.getDate() + 1);
        }
        this.data.labels = Array.from(dates, (d) => d.toLocaleDateString());

        //prepare datasets for search terms
        this.data.datasets = [];
        let searchTerms = val.reduce((acc, cur) => { if(!acc.includes(cur.searchTerm)) {acc.push(cur.searchTerm)} return acc}, [] as string[]);
        searchTerms.forEach((term) => {
          let dataset =
          {
            label: term,
            backgroundColor: 'rgba(' + 
              Math.floor(Math.random()*255)+ ','+
              Math.floor(Math.random()*255)+ ','+
              Math.floor(Math.random()*255)+ ',0.5)',
            data: []
          }
          console.log(term);
          let searchTermResults = val.filter((s) => s.searchTerm == term);
          dates.forEach((d) => {
            let searchTermDates = searchTermResults.filter((s) => new Date(s.time).toDateString() == d.toDateString());
            let ranking = searchTermDates.reduce((a,b) => { return (b.ranking != 0 & a > b.ranking) ? b.ranking : a ; },2000);
            if(ranking==2000) ranking = 0;
            console.log(ranking);
            dataset.data.push(ranking);
          })
          this.data.datasets.push(dataset);
        });
      }
    }
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


<!-- <style>
@media (min-width: 1024px) {
  .about {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style> -->
