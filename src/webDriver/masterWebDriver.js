import 'chromedriver'
import WebDriver from 'selenium-webdriver'

const URL = 'https://www.jusbrasil.com.br/jurisprudencia/busca?q=assédio+2017'
const driver = new WebDriver.Builder().forBrowser('chrome').build()

const MasterWebDriver = () => {
  initializaeBrowser()
  fetchUrl()
}

const initializaeBrowser = async () => {
  try {
    await driver.manage().deleteAllCookies()
  } catch (error) {
    console.log('Server error: Ocorreu um erro inesperado na inicialização do arquivo binário respectivo ao navegador.')
    console.log('Erro interno: ', error)
    await driver.close()
  }
}

const fetchUrl = async () => {
  try {
    await driver.get(URL)
    console.log('Success: ', URL)
  } catch (error) {
    console.log('ERROR: ', error)
    await driver.close()
  } finally {
    await driver.close()
    console.log('Close browser.')
  }
}

export default MasterWebDriver
