import express from 'express'
import MasterWebDriver from './webDriver/masterWebDriver'

const app = express()

const port = 3000

app.listen(port, () => {
  console.log('Started on port ', port)
  MasterWebDriver()
})

export default app
