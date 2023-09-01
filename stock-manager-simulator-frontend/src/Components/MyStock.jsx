import { Navbar } from './Navbar'
import { PermissionForComponent } from './Functions/PermissionForComponent'

export const MyStock = () => {
  PermissionForComponent()


  return (
    <div>
        <Navbar/>
    </div>
  )
}
