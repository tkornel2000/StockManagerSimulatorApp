import React from 'react'

export const WelcomeUser = () => {
  const currentUser = JSON.parse(localStorage.getItem("currentUser"));
  console.log(currentUser)
  return (
    <div>
        <h3>Üdvözöllek {currentUser.lastname} {currentUser.firstname}!</h3>
        <p>Rendelkezésre álló pénzed: {currentUser.money}</p>
        <p>Részvényekben lévő pénzed: {currentUser.stockValue}</p>
        <p>Portfoliód teljes értéke: {currentUser.money+currentUser.stockValue}</p>
    </div>
  )
}
