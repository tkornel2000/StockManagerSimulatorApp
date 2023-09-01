import { Navbar } from './Navbar'
import { PermissionForComponent } from './Functions/PermissionForComponent'

export const Stocks = () => {
  PermissionForComponent()

  return (
    <div>
        <Navbar/>
    </div>
  )
}
