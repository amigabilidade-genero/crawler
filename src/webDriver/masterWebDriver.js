import 'chromedriver'
import chrome from 'selenium-webdriver/chrome'
import { Builder, By } from 'selenium-webdriver'
import versionSite from '../versionSite/version'

const URL = 'https://www.jusbrasil.com.br/jurisprudencia/busca?q=assédio+Belo+Horizonte+2017'

const chromeOptions = new chrome.Options()
chromeOptions.addArguments('test-type')
chromeOptions.addArguments('start-maximized')
chromeOptions.addArguments('--js-flags=--expose-gc')
chromeOptions.addArguments('--enable-precise-memory-info')
chromeOptions.addArguments('--disable-popup-blocking')
chromeOptions.addArguments('--disable-default-apps')
chromeOptions.addArguments('--disable-infobars')
chromeOptions.addArguments('--incognito')
chromeOptions.addArguments('--disable-extensionss')
chromeOptions.addArguments('--no-sandbox')
chromeOptions.addArguments('test-type=browser')

const driver = new Builder()
  .forBrowser('chrome')
  .withCapabilities(chromeOptions)
  .build()
var VERSION = ''

const MasterWebDriver = () => {
  initializaeBrowser()
  fetchUrl()
  versionSite(VERSION)
}

const initializaeBrowser = async () => {
  try {
    await driver.manage().deleteAllCookies()
  } catch (error) {
    console.log('Server error: Ocorreu um erro inesperado na inicialização do arquivo binário respectivo ao navegador.'
    )
    console.log('Erro interno: ', error)
    await driver.close()
  }
}

const fetchUrl = async () => {
  try {
    await driver.get(URL)
    console.log('Success: ', URL)
    initV1()
  } catch (ERROR) {
    console.log('ERROR: ', ERROR)
  } finally {
    await driver.close()
  }
}

const initV1 = async () => {
  try {
    var searchForm = await driver.findElement(
      By.className('SearchResults-documents')
    )
    console.log(searchForm)
  } catch (ERROR) {
    console.log('Old version faill')
    console.log('Call new version')
    initV2()
  }
}

const initV2 = async () => {
  try {
    console.log('New version change')
    var searchForm = await driver.findElement(By.className('DocumentSnippet'))
    console.log(searchForm)
  } catch (ERROR) {
    console.log('New version faill')
    console.log('ERROR: ', ERROR)
  }
}

export default MasterWebDriver
