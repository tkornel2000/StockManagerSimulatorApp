import { Navbar } from './Navbar'
import { PermissionForComponent } from './Functions/PermissionForComponent'

export const Rank = () => {
  PermissionForComponent()

  return (
    <div>
        <Navbar/>
    </div>
  )
}
