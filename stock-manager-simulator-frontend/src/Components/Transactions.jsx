import { Navbar } from './Navbar'
import { PermissionForComponent } from './Functions/PermissionForComponent'

export const Transactions = () => {
  PermissionForComponent()

  return (
    <div>
      <Navbar/>
    </div>
  )
}
